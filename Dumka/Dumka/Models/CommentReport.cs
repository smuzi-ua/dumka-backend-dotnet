using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class CommentReport
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ReportCategory { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Comment CommentNavigation { get; set; }
        public virtual ReportCategory ReportCategoryNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
