using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class ProposalLikes
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int UserId { get; set; }
        public int FeedbackId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual FeedbackTypes Feedback { get; set; }
        public virtual Proposals Proposal { get; set; }
        public virtual Users User { get; set; }
    }
}
