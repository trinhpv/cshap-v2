using Shoping.Model.Models;
using Shoping.Model.Types.Order;
using Shoping.Model.Types.Vote;
using Shopping.Utility;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface IOrderService 
    {
        Task PlaceOrder(int userId);
        Task<IEnumerable<Order>> GetMyOrders(int userId);
        Task<GetListOrdersWithPaginateResponse> GetOrders(PaginationFilter paginateData);

        void UpdateStatus(PaymentStatus status, int id);
    }
}
