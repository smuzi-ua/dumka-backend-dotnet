using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class ProposalDeadlineType
    {
        public ProposalDeadlineType()
        {
            Proposals = new HashSet<Proposal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}
