using System;
using System.Collections.Generic;

namespace Dumka
{
    public partial class School
    {
        public School()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Display { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
