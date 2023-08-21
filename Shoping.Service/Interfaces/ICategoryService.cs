using Shoping.Model.Models;
using Shoping.Model.Types.Category;
using Shopping.Data.Entities;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface ICategoryService : IBaseService<Category>
    {
        GetCategoryResponse Detail(int id);
        Task<GetListCateWithPaginateResponse> GetList(PaginationFilter paginateData);
    }
}
