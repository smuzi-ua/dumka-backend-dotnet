using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class FeedbackType
    {
        public FeedbackType()
        {
            CommentLikes = new HashSet<CommentLike>();
            ProposalLikes = new HashSet<ProposalLike>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual ICollection<ProposalLike> ProposalLikes { get; set; }
    }
}
