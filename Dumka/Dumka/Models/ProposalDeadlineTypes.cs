using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class ProposalDeadlineTypes
    {
        public ProposalDeadlineTypes()
        {
            Proposals = new HashSet<Proposals>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Proposals> Proposals { get; set; }
    }
}
