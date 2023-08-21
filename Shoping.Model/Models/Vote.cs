using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Models
{
    public class Vote
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Size { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedById { get; set; }
        public SimpleUser CreatedBy { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }

    public class SimpleVote
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Size { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedById { get; set; }
        public SimpleUser CreatedBy { get; set; } = null!;
    }
}
