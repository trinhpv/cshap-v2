using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public SimpleUser User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public float UnitPrice { get; set; } = 0;
        public float TotalPrice { get; set; } = 0;
    }
}
