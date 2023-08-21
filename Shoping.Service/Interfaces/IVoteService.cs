using Shoping.Model.Models;
using Shoping.Model.Types.Vote;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface IVoteService : IBaseService<Vote>
    {
         Task CreateVote(Vote obj);
        Task<GetListVotesWithPaginateResponse> GetVotesByProduct(PaginationFilter paginationFilter, int productId);
    }
}
