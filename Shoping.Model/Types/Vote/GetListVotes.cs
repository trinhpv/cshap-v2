using Shoping.Model.Models;
using Shoping.Model.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Vote
{
    public class GetListVotesWithPaginateResponse
    {
        public IEnumerable<SimpleVote> Data { get; set; }
        public int TotalCount { get; set; }
        public int ToTalVote { get; set; }
        public double AvgVote { get; set; }
    }

    public class PagedResponseForVote<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int ToTalVote { get; set; }
        public double AvgVote { get; set; }
        public PagedResponseForVote(T data, int pageNumber, int pageSize, int totaRecord, double avgVote)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalRecords = totaRecord;
            this.TotalPages = (int)Math.Ceiling((double)totaRecord / (double)pageSize);
            this.Data = data;
            this.AvgVote = avgVote;
            this.ToTalVote = totaRecord;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
