using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Models.DTO
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string School { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
        public DateTime? BannedTo { get; set; }
        public bool? Verified { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int CommentsCount { get; set; }
        public int ProposalsCount { get; set; }
    }
}
