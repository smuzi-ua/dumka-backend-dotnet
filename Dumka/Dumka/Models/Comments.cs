using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class Comments
    {
        public Comments()
        {
            CommentLikes = new HashSet<CommentLikes>();
            CommentReports = new HashSet<CommentReports>();
        }

        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public bool Anonymous { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Proposals Proposal { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<CommentLikes> CommentLikes { get; set; }
        public virtual ICollection<CommentReports> CommentReports { get; set; }
    }
}
