using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class Proposal
    {
        public Proposal()
        {
            Comments = new HashSet<Comment>();
            ProposalLikes = new HashSet<ProposalLike>();
            ProposalReports = new HashSet<ProposalReport>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Anonymous { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int? StageId { get; set; }
        public int? DeadlineId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ProposalDeadlineType Deadline { get; set; }
        public virtual ProposalStageTypes Stage { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProposalLike> ProposalLikes { get; set; }
        public virtual ICollection<ProposalReport> ProposalReports { get; set; }
    }
}
