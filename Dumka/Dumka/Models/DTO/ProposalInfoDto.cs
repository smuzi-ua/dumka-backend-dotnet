using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Models.DTO
{
    public class ProposalInfoDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserNickname { get; set; }
        public bool Anonymous { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? StageId { get; set; }
        public string Stage { get; set; }
        public int? DeadlineId { get; set; }
        public string Deadline { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public bool IsDislikedByCurrentUser { get; set; }
        public int CommentsCount { get; set; }
        public List<CommentInfoDto> Comments { get; set; }
    }
}
