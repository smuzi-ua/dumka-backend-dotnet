using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class CommentLike
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int FeedbackId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual FeedbackType Feedback { get; set; }
        public virtual User User { get; set; }
    }
}
