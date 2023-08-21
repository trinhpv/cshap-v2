using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Comment;
using Shoping.Model.Types.Paginating;
using Shoping.Model.Types.Vote;
using Shoping.Model.Wrapper;
using Shopping.Service.Interfaces;
using Shopping.Service.Services;
using Shopping.Utility.Filter;
using System.Data;
using System.Security.Claims;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }
        [Route("/[controller]/Create")]
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<Response<bool>> Create([FromBody] CreateVoteRequest voteData)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var vote = new Vote
            {
                Size = voteData.Size,
                ProductId = voteData.ProductId,
                CreatedById = int.Parse(identity.FindFirst("UserId").Value)
            };
             await  _voteService.CreateVote(vote);

            return new Response<bool>(true);

        }


        [Route("/[controller]/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public Response<bool> Delete(int id)
        {

            _voteService.Delete(id);

            return new Response<bool>(true);

        }

        [Route("/[controller]/GetByProduct/{productId}")]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<PagedResponseForVote<IEnumerable<SimpleVote>>> GetByProduct([FromQuery] PaginateParams paginateData, int productId)
        {
            var paginateFilter = new PaginationFilter
            {
                PageNumber = paginateData.PageNumber,
                PageSize = paginateData.PageSize,
            };
            var result = await _voteService.GetVotesByProduct(paginateFilter, productId);

            return new PagedResponseForVote<IEnumerable<SimpleVote>>(result.Data, paginateFilter.PageNumber, paginateFilter.PageSize, result.TotalCount, result.AvgVote);

        }
    }
}
