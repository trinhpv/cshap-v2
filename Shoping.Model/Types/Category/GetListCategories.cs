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

namespace Shoping.Model.Types.Category
{
    public class GetListCateWithPaginateResponse
    {
        public IEnumerable<GetListCategoryResponse> Data { get; set; }
        public int TotalCount { get; set; }
    }
    public class GetListCategoryResponse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedById { get; set; }
        public SimpleUser CreatedBy { get; set; } = null!;

    }
   


    public class GetCategoriesPaginateParams : PaginateParams
    {
        public OrderRequest OrderBy { get; set; }
    }


    public enum OrderRequest
    {
        [EnumMember(Value = "name")]
        Name ,
        [EnumMember(Value = "createdDate")]
        CreatedDate
    }

}
