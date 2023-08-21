using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Paginating
{
    public class PaginateParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchString { get; set; } = string.Empty;
        //public virtual string OrderBy { get; set; } = string.Empty;
        public OrderType OrderType { get; set; } = OrderType.Ascending;
    }
}
