using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Data.Entities
{
    public class CartEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int ProductId { get; set; }  
        public ProductEntity Product { get; set; }

        public int Quantity { get; set; } 
        public float UnitPrice { get; set; } = 0;
        public float TotalPrice { get; set; } = 0;
    }
}
