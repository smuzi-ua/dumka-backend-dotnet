using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class Users
    {
        public Users()
        {
            CommentLikes = new HashSet<CommentLikes>();
            CommentReports = new HashSet<CommentReports>();
            Comments = new HashSet<Comments>();
            ProposalLikes = new HashSet<ProposalLikes>();
            ProposalReports = new HashSet<ProposalReports>();
            Proposals = new HashSet<Proposals>();
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

        public virtual Schools School { get; set; }
        public virtual UserTypes UserType { get; set; }
        public virtual ICollection<CommentLikes> CommentLikes { get; set; }
        public virtual ICollection<CommentReports> CommentReports { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<ProposalLikes> ProposalLikes { get; set; }
        public virtual ICollection<ProposalReports> ProposalReports { get; set; }
        public virtual ICollection<Proposals> Proposals { get; set; }
    }
}
