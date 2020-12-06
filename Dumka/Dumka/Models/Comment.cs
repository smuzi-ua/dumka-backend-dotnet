using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class Comment
    {
        public Comment()
        {
            CommentLikes = new HashSet<CommentLike>();
            CommentReports = new HashSet<CommentReport>();
        }

        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int UserId { get; set; }
        public string CommentStr { get; set; }
        public bool Anonymous { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Proposal Proposal { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<CommentReport> CommentReports { get; set; }
    }
}
