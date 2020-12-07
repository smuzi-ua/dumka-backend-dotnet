using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class User
    {
        public User()
        {
            CommentLikes = new HashSet<CommentLike>();
            CommentReports = new HashSet<CommentReport>();
            Comments = new HashSet<Comment>();
            ProposalLikes = new HashSet<ProposalLike>();
            ProposalReports = new HashSet<ProposalReport>();
            Proposals = new HashSet<Proposal>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public decimal? Code { get; set; }
        public DateTime? CodeGenerated { get; set; }
        public int UserTypeId { get; set; }
        public DateTime? BannedTo { get; set; }
        public bool? Verified { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual School School { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<CommentReport> CommentReports { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProposalLike> ProposalLikes { get; set; }
        public virtual ICollection<ProposalReport> ProposalReports { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}
