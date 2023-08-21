using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Auth;
using Shoping.Model.Types.Category;
using Shoping.Model.Types.Paginating;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using Shopping.Service.Services;
using Shopping.Utility.Filter;
using System.Security.Claims;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Route("/[controller]/Create")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Create([FromBody] CreateCategoryRequest cate)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var newCateData = new Category
            {
                Name = cate.Name,
                Description = cate.Description,
                CreatedById = int.Parse(identity.FindFirst("UserId").Value)
            };
            _categoryService.Create(newCateData);
            return new Response<bool>(true);
        }
        [Route("/[controller]/Update/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Update([FromBody] CreateCategoryRequest cate, int id)
        {
            var newCateData = new Category
            {
                Name = cate.Name,
                Description = cate.Description,
            };
            _categoryService.Update(newCateData, id);
            return new Response<bool>(true);
        }
        [Route("/[controller]/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Delete(int id)
        {
            _categoryService.Delete(id);
            return new Response<bool>(true);
        }
        [Route("/[controller]/Detail/{id}")]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public Response<GetCategoryResponse> Detail(int id)
        {
            var result = _categoryService.Detail(id);
            return new Response<GetCategoryResponse>(result);
        }
        [Route("/[controller]/GetList")]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<PagedResponse<IEnumerable<GetListCategoryResponse>>> GetList([FromQuery] GetCategoriesPaginateParams paginateParams)
        {
            var paginateData = new PaginationFilter
            {
                PageNumber = paginateParams.PageNumber,
                PageSize = paginateParams.PageSize,
                SearchString = paginateParams.SearchString,
                OrderBy = paginateParams.OrderBy.ToString(),
                OrderType = paginateParams.OrderType,

            };
            var result = await _categoryService.GetList(paginateData);
            return new PagedResponse<IEnumerable<GetListCategoryResponse>>(result.Data, paginateData.PageNumber, paginateData.PageSize, result.TotalCount);
        }

    }
}
