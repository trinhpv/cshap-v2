using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Cart;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using System.Data;
using System.Security.Claims;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Route("/[controller]/GetOwn")]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<Response<IEnumerable<Cart>>> GetOwn()
        {
            var result = await _cartService.GetMyCart(getUserId());
            return new Response<IEnumerable<Cart>>(result);
        }

        [Route("/[controller]/Add")]
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<Response<string>> AddToCart([FromBody] AddToCartRequest addData)
        {
            var addToCartData = new Cart
            {
                UserId = getUserId(),
                ProductId = addData.ProductId,
                Quantity = addData.Quantity,
            };
             await _cartService.Add(addToCartData);
            return new Response<string>("success");
        }

        [Route("/[controller]/Remove")]
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public string RemoveToCart()
        {
            return "oke";
        }




        private int getUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return int.Parse(identity.FindFirst("UserId").Value);
        }

    }
}
