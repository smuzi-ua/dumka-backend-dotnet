using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class ReportCategories
    {
        public ReportCategories()
        {
            CommentReports = new HashSet<CommentReports>();
            ProposalReports = new HashSet<ProposalReports>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ICollection<CommentReports> CommentReports { get; set; }
        public virtual ICollection<ProposalReports> ProposalReports { get; set; }
    }
}
