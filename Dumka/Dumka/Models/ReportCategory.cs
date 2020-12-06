using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class ReportCategory
    {
        public ReportCategory()
        {
            CommentReports = new HashSet<CommentReport>();
            ProposalReports = new HashSet<ProposalReport>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ICollection<CommentReport> CommentReports { get; set; }
        public virtual ICollection<ProposalReport> ProposalReports { get; set; }
    }
}
