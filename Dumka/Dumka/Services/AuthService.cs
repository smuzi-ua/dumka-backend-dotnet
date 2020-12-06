using Dumka.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dumka.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly DumkaDbContext _dbContext;
        private readonly UserService _userService;

        private const string SCHOOLID = "SchoolId";
        private const string USERID = "UserId";
        private const string USERTYPEID = "UserTypeId";

        public AuthService(IConfiguration configuration, UserService userService,
                           DumkaDbContext dbContext)
        {
            _configuration = configuration;
            _userService = userService;
            _dbContext = dbContext;
        }

        public dynamic CreateJwtToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: _configuration["Jwt:ValidIssuer"],
                    audience: _configuration["Jwt:ValidAudience"],
                    notBefore: now,
                    claims: identity.Claims,
                    //expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                        SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return response;
        }

        public async Task<Tuple<ClaimsIdentity, string>> GetIdentity(LoginDto loginDto)
        {
            var userDtoTuple = await _userService.CheckOrCreate(loginDto);
            if (userDtoTuple == null)
            {
                return null;
            }
            if (userDtoTuple.Item2 != null || userDtoTuple.Item1 == null)
            {
                return new Tuple<ClaimsIdentity, string>(null, userDtoTuple.Item2);
            }
            var userDto = userDtoTuple.Item1;
            var claims = new List<Claim>
                {
                    new Claim(USERID, userDto.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userDto.Nickname),
                    new Claim(SCHOOLID, userDto.SchoolId.ToString()),
                    new Claim(USERTYPEID, userDto.UserTypeId.ToString()),
                    new Claim(ClaimTypes.Role, userDto.UserType)
                };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimTypes.Name,
                    ClaimTypes.Role);
            return new Tuple<ClaimsIdentity, string>(claimsIdentity, null);
        }

        public int? GetSchoolId(IEnumerable<Claim> claims)
        {
            string schoolIdStr = claims.FirstOrDefault(_ => _.Type == SCHOOLID)?.Value;
            int schoolId;
            if (schoolIdStr == null || !int.TryParse(schoolIdStr, out schoolId))
            {
                return null;
            }
            return schoolId;
        }

        public int? GetUserId(IEnumerable<Claim> claims)
        {
            string userIdStr = claims.FirstOrDefault(_ => _.Type == USERID)?.Value;
            int userId;
            if (userIdStr == null || !int.TryParse(userIdStr, out userId))
            {
                return null;
            }
            return userId;
        }

        public int? GetUserTypeId(IEnumerable<Claim> claims)
        {
            string userTypeIdStr = claims.FirstOrDefault(_ => _.Type == USERTYPEID)?.Value;
            int userTypeId;
            if (userTypeIdStr == null || !int.TryParse(userTypeIdStr, out userTypeId))
            {
                return null;
            }
            return userTypeId;
        }
    }
}
