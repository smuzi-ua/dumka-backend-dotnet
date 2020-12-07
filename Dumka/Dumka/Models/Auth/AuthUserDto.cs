using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Models.Auth
{
    public class AuthUserDto
    {
        public string Nickname { get; set; }
        public int UserId { get; set; }
        public int SchoolId { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
    }
}
