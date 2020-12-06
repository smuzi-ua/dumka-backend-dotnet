using Dumka.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Services
{
    public class UserService
    {
        private readonly DumkaDbContext _dbContext;

        public UserService(DumkaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tuple<UserDto, string>> CheckOrCreate(LoginDto loginDto)
        {
            var isSchoolExist = await _dbContext.Schools.AnyAsync(_ => _.Id == loginDto.SchoolId);
            if (!isSchoolExist)
            {
                return new Tuple<UserDto, string>(
                    null,
                    "School doesn't exist"
                );
            }
            var user = await _dbContext.Users.Include(_ => _.UserType)
                    .FirstOrDefaultAsync(_ => _.Nickname == loginDto.Nickname &&
                                    _.SchoolId == loginDto.SchoolId);
            if (user != null)
            {
                if (user.Code != loginDto.Code)
                {
                    return null;
                }
                if ((loginDto.UserTypeId != null && loginDto.UserTypeId != user.UserTypeId) ||
                    (loginDto.UserType != null && loginDto.UserType != user.UserType.Name))
                {
                    return new Tuple<UserDto, string>(
                        null,
                        "User role is incorrect"
                    );
                }
                if (user.Name != loginDto.Name)
                {
                    user.Name = loginDto.Name;
                    user.DateModified = DateTime.Now;
                    _dbContext.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                return new Tuple<UserDto, string>(
                    new UserDto
                    {
                        UserId = user.Id,
                        Nickname = user.Nickname,
                        SchoolId = user.SchoolId,
                        UserTypeId = user.UserTypeId,
                        UserType = user.UserType.Name
                    },
                    null
                );
            }
            else
            {
                user = new User
                {
                    Name = loginDto.Name,
                    Nickname = loginDto.Nickname,
                    SchoolId = loginDto.SchoolId,
                    Code = loginDto.Code,
                    CodeGenerated = DateTime.Now,
                    DateModified = DateTime.Now,
                    DateCreated = DateTime.Now
                };

                var userType = await _dbContext.UserTypes.FirstOrDefaultAsync(_ =>
                    _.Id == loginDto.UserTypeId || _.Name == loginDto.UserType);
                if ((loginDto.UserTypeId != null || loginDto.UserType != null) &&
                    userType == null)
                {
                    return new Tuple<UserDto, string>(
                        null,
                        "User type not found"
                    );
                }
                if (loginDto.UserTypeId != null && loginDto.UserType != null)
                {
                    if (userType.Id != loginDto.UserTypeId || userType.Name != loginDto.UserType)
                    {
                        return new Tuple<UserDto, string>(
                            null,
                            "User type is inconsistent"
                        );
                    }
                }
                if (userType == null)
                {
                    userType = new UserType
                    {
                        Id = 1
                    };
                }
                user.UserTypeId = userType.Id;
                _dbContext.Add(user);
                await _dbContext.SaveChangesAsync();
                return new Tuple<UserDto, string>(
                    new UserDto
                    {
                        UserId = user.Id,
                        Nickname = user.Nickname,
                        SchoolId = user.SchoolId,
                        UserTypeId = user.UserTypeId,
                        UserType = user.UserType.Name,
                    },
                    null
                );
            }
        }
    }
}
