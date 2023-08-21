using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Utility.Filter
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public string SearchBy { get; set;} = string.Empty;
        public OrderType OrderType { get; set; } = OrderType.Ascending;
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize, string searchBy, string searchString, string orderBy, OrderType orderType)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
            this.SearchString = searchString;
            this.OrderBy = orderBy;
            this.OrderType = orderType;
            this.SearchBy = searchBy;
        }
    }
}
