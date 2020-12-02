using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class FeedbackTypes
    {
        public FeedbackTypes()
        {
            CommentLikes = new HashSet<CommentLikes>();
            ProposalLikes = new HashSet<ProposalLikes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CommentLikes> CommentLikes { get; set; }
        public virtual ICollection<ProposalLikes> ProposalLikes { get; set; }
    }
}
