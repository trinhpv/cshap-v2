using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Price { get; set; }
        public string FirstImage { get; set; } = "";
        public string SecondImage { get; set; } = "";
        public string ThirdImage { get; set; } = "";
        public string LastImage { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public SimpleCategory Category { get; set; } = null!;

        public int CreatedById { get; set; }
        public SimpleUser CreatedBy { get; set; } = null!;

        //public virtual ICollection<SimpleComment> Comments { get; } = new List<SimpleComment>();
        //public virtual ICollection<SimpleVote> Votes { get; } = new List<SimpleVote>();

    }
    public class SimpleProduct
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public string FirstImage { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }

    }
}
