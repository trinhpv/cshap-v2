using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Vote
{
    public class CreateVoteRequest
    {
        [Required]
        [Range(1, 5)]
        public int Size { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
