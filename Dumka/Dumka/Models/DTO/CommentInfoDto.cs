using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Models.DTO
{
    public class CommentInfoDto
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int UserId { get; set; }
        public string UserNickname { get; set; }
        public bool Anonymous { get; set; }
        public string CommentStr { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public bool IsDislikedByCurrentUser { get; set; }
    }
}
