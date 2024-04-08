using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Order;
using Shoping.Model.Types.Vote;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using Shopping.Service.Services;
using Shopping.Utility.Filter;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Route("/[controller]/Place")]
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<Response<bool>> PlaceOrder()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            await _orderService.PlaceOrder(int.Parse(identity.FindFirst("UserId").Value));

            return new Response<bool>(true);
        }

        [Route("/[controller]/GetOwn")]
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<Response<IEnumerable<Order>>> GetOwn()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var result = await _orderService.GetMyOrders(int.Parse(identity.FindFirst("UserId").Value));

            return new Response<IEnumerable<Order>>(result);

        }

        [Route("/[controller]/UpdateStatus/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> UpdateStatus(UpdateStatusRequest newStatus, int id)
        {
            _orderService.UpdateStatus(newStatus.Status, id);
            return new Response<bool>(true);
        }

        [Route("/[controller]/GetList")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<PagedResponse<IEnumerable<OrderResponseItem>>> GetList([FromQuery] GetOrdersPaginateParams paginateParams)
        {
            var paginateData = new PaginationFilter
            {
                PageNumber = paginateParams.PageNumber,
                PageSize = paginateParams.PageSize,
                SearchBy = paginateParams.SearchBy.ToString(),
                SearchString = paginateParams.SearchString,
                OrderBy = paginateParams.OrderBy.ToString(),
                OrderType = paginateParams.OrderType,

            };

            var result = await _orderService.GetOrders(paginateData);
            return new PagedResponse<IEnumerable<OrderResponseItem>>(result.Data, paginateData.PageNumber, paginateData.PageSize, result.TotalCount);


        }
    }
}
