using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dumka;
using Dumka.Models.Auth;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Dumka.Services;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Dumka.Models.DTO;

namespace Dumka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DumkaDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        public UsersController(DumkaDbContext context, IConfiguration configuration,
                               AuthService authService, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]LoginDto loginDto)
        {
            var identityTuple = await _authService.GetIdentity(loginDto);
            if (identityTuple == null)
            {
                return Forbid();
            }
            if (identityTuple.Item2 != null || identityTuple.Item1 == null)
            {
                return BadRequest(new { errorText = identityTuple.Item2 });
            }
            var identity = identityTuple.Item1;
            var response = _authService.CreateJwtToken(identity);

            return new JsonResult(response);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            int? schoolId = _authService.GetSchoolId(User.Claims);
            if (schoolId == null)
            {
                return BadRequest();
            }
            var users = await _context.Users.Include(_ => _.UserType)
                .Include(_ => _.School)
                .Include(_ => _.Proposals)
                .Include(_ => _.Comments)
                .Where(_ => _.SchoolId == schoolId.Value)
                .ToListAsync();
            IEnumerable<UserInfoDto> userInfos = users.Select(_ => _mapper.Map<UserInfoDto>(_));
            return new JsonResult(userInfos);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            int? schoolId = _authService.GetSchoolId(User.Claims);
            if (schoolId == null)
            {
                return BadRequest();
            }
            var user = await _context.Users.Include(_ => _.UserType)
                .Include(_ => _.School)
                .Include(_ => _.Proposals)
                .Include(_ => _.Comments)
                .FirstOrDefaultAsync(_ => _.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            if (user.SchoolId != schoolId)
            {
                return Forbid();
            }
            return new JsonResult(_mapper.Map<UserInfoDto>(user));
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, User users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUsers(User users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
