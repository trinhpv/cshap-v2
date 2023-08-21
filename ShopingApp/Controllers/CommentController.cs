using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Category;
using Shoping.Model.Types.Comment;
using Shoping.Model.Types.Paginating;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using Shopping.Utility.Filter;
using System.Data;
using System.Security.Claims;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Route("/[controller]/Create")]
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public Response<bool> Create([FromBody] CreateCommentRequest commentData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var newCommentData = new Comment
            {
                Content = commentData.Content,
                ProductId = commentData.ProductId,
                CreatedById = int.Parse(identity.FindFirst("UserId").Value)
            };
            _commentService.Create(newCommentData);

            return new Response<bool>(true);

        }

        [Route("/[controller]/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Delete(int id)
        {
            
            _commentService.Delete(id);

            return new Response<bool>(true);

        }

        [Route("/[controller]/GetByProduct/{productId}")]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<PagedResponse<IEnumerable<SimpleComment>>> GetByProduct([FromQuery] PaginateParams paginateData, int productId)
        {
            var paginateFilter = new PaginationFilter
            {
                PageNumber = paginateData.PageNumber,
                PageSize = paginateData.PageSize,
            };
            var result = await _commentService.GetCommentsByProductId(paginateFilter, productId);

            return new PagedResponse<IEnumerable<SimpleComment>>(result.Data, paginateFilter.PageNumber, paginateFilter.PageSize, result.TotalCount);

        }
    }
}
