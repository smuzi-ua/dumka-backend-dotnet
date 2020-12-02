using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class CommentReports
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ReportCategory { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Comments CommentNavigation { get; set; }
        public virtual ReportCategories ReportCategoryNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
