using Newtonsoft.Json.Linq;
using Shoping.Model.Models;
using Shoping.Model.Types.Paginating;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Product
{
    public class GetListProductsWithPaginateResponse
    {
        public IEnumerable<ProductResponseItem> Data { get; set; }
        public int TotalCount { get; set; }
    }
    public class ProductResponseItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? FirstImage { get; set; }
        public string? SecondImage { get; set; }
        public string? ThirdImage { get; set; }
        public string? LastImage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public SimpleCategory Category { get; set; } = null!;
        public int CreatedById { get; set; }
        public SimpleUser CreatedBy { get; set; } = null!;
    }



    public class GetProductsPaginateParams : PaginateParams
    {
        public OrderRequestProduct OrderBy { get; set; }
        public SearchByRequestProduct SearchBy { get; set; }
    }


    public enum OrderRequestProduct
    {
        Name,
        CreatedDate,
        Price,
        CategoryId
    }
    public enum SearchByRequestProduct
    {
        Name,
        CategoryId,
        LessThanPrice,
        GreaterThanPrice,
    }
}
