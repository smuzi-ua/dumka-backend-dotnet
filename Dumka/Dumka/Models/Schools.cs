using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class Schools
    {
        public Schools()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Display { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
