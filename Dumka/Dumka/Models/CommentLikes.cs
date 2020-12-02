using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class CommentLikes
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int FeedbackId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Comments Comment { get; set; }
        public virtual FeedbackTypes Feedback { get; set; }
        public virtual Users User { get; set; }
    }
}
