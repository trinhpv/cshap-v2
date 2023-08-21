using Shoping.Model.Models;
using Shoping.Model.Types.Category;
using Shoping.Model.Types.Product;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface IProductService: IBaseService<Product>
    {
        Task<GetListProductsWithPaginateResponse> GetList(PaginationFilter paginateData);
        Task<Product> Detail(int id);
    }
}
