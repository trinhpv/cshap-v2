using Shoping.Model.Models;
using Shoping.Model.Types.Comment;
using Shopping.Data.Entities;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface ICommentService : IBaseService<Comment>
    {
         Task<GetListCommentsWithPaginateResponse> GetCommentsByProductId(PaginationFilter paginateData, int productId);
    }
}
