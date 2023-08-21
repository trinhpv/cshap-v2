using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Vote;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using Shopping.Service.Services;
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
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<Response<bool>> PlaceOrder()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            await _orderService.PlaceOrder(int.Parse(identity.FindFirst("UserId").Value));

            return new Response<bool>(true);

        }
    }
}
