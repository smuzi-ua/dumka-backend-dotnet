using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class ProposalReports
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public int UserId { get; set; }
        public int ReportCategory { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Proposals Proposal { get; set; }
        public virtual ReportCategories ReportCategoryNavigation { get; set; }
        public virtual Users User { get; set; }
    }
}
