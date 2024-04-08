using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Models
{
    public class Report
    {
        public float TotalOrders { get; set; }
        public int OrderCount { get; set; } = 0;
        public ReportTimeRange TimeRange { get; set; }
        public required string StringTime { get; set; }    
        public int PairedOrder { get; set; } = 0;
        public int PendingOrder { get; set; } = 0;

    }
}
