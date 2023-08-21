using Shoping.Model.Models;
using Shoping.Model.Types.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Comment
{
    public class GetListCommentsWithPaginateResponse
    {
        public IEnumerable<SimpleComment> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
