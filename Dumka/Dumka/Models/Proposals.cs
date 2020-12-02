using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class Proposals
    {
        public Proposals()
        {
            Comments = new HashSet<Comments>();
            ProposalLikes = new HashSet<ProposalLikes>();
            ProposalReports = new HashSet<ProposalReports>();
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

        public virtual ProposalDeadlineTypes Deadline { get; set; }
        public virtual ProposalStageTypes Stage { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<ProposalLikes> ProposalLikes { get; set; }
        public virtual ICollection<ProposalReports> ProposalReports { get; set; }
    }
}
