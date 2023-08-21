using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Category;
using Shoping.Model.Types.Product;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using Shopping.Service.Services;
using Shopping.Utility.Filter;
using System.Data;
using System.Security.Claims;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("/[controller]/Create")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Create([FromBody] CreateProductRequest product)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var newProductData = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FirstImage = product.FirstImage,
                LastImage = product.LastImage,
                SecondImage = product.SecondImage,
                ThirdImage = product.ThirdImage,
                CategoryId = product.CategoryId,
                CreatedById = int.Parse(identity.FindFirst("UserId").Value)
            };
            _productService.Create(newProductData);
            return new Response<bool>(true);
        }
        [Route("/[controller]/Update/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Update([FromBody] CreateProductRequest product, int id)
        {
            var updatedProductData = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FirstImage = product.FirstImage,
                LastImage = product.LastImage,
                SecondImage = product.SecondImage,
                ThirdImage = product.ThirdImage,
                CategoryId = product.CategoryId,
            };
            _productService.Update(updatedProductData, id);
            return new Response<bool>(true);
        }

        [Route("/[controller]/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Delete(int id)
        {
            _productService.Delete(id);
            return new Response<bool>(true);
        }

        [Route("/[controller]/GetList")]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<PagedResponse<IEnumerable<ProductResponseItem>>> GetList([FromQuery] GetProductsPaginateParams paginateParams)
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
            var result = await _productService.GetList(paginateData);
            return new PagedResponse<IEnumerable<ProductResponseItem>>(result.Data, paginateData.PageNumber, paginateData.PageSize, result.TotalCount);
        }
        [Route("/[controller]/Detail/{id}")]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<Response<Product>> Detail(int id)
        {
            var result = await _productService.Detail(id);
            return new Response<Product>(result);
        }



    }
}
