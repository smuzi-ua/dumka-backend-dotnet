using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dumka.Models.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public bool Anonymous { get; set; }
        public string CommentStr { get; set; }
    }
}
