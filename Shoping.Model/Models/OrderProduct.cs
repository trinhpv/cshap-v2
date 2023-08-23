using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
    public class SimpleOrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public SimpleProduct Product { get; set; } = null!;
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }

}
