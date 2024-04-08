using Shopping.Data.Repositories.Interfaces;
using Shopping.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Services
{
    public class ReportService : IReportService
    {
        private IOrderRepository _orderRepository;
        public ReportService(IOrderRepository orderRepository) {
            _orderRepository = orderRepository;
        }

    }
}
