using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductIdsString
        {
            get => string.Join(';', ProductIds);
            set => ProductIds = value == null ? Array.Empty<int>() : Array.ConvertAll(value.Split(';', StringSplitOptions.RemoveEmptyEntries), int.Parse);
        }

        [NotMapped]
        public required int[] ProductIds { get; set; }

        public float TotalPay { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedById { get; set; }
        public SimpleUser CreatedBy { get; set; } = null!;
        public List<OrderProduct> OrderProducts { get; } = new();
    }
}
