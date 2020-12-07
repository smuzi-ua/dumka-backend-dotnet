using System;
using System.Collections.Generic;

namespace Dumka.Models.DTO
{
    public partial class SchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Display { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
