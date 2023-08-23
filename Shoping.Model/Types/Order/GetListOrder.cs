using Shoping.Model.Models;
using Shoping.Model.Types.Paginating;
using Shoping.Model.Types.Product;
using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Order
{
    public class GetListOrdersWithPaginateResponse
    {
        public IEnumerable<OrderResponseItem> Data { get; set; }
        public int TotalCount { get; set; }
    }

    public class OrderResponseItem
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
        public SimpleUser CreatedBy { get; set; } = null!;
        public List<SimpleOrderProduct> OrderProducts { get; } = new();
    }

    public class GetOrdersPaginateParams : PaginateParams
    {
        public OrderRequestOrder OrderBy { get; set; }
        public SearchByRequestOrder SearchBy { get; set; }
    }


    public enum OrderRequestOrder
    {
        CreatedDate,
        TotalPay,
        PaymentStatus
    }
    public enum SearchByRequestOrder
    {
        LessThanTotalPay,
        GreaterThanTotalPay,
        CreatedDate,
        PaymentStatus,
        UserId
    }
}
