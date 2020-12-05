using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Models.Auth
{
    public class LoginDto
    {
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int SchoolId { get; set; }
        public long Code { get; set; }
        public int? UserTypeId { get; set; }
        public string UserType { get; set; }
    }
}
