using Shoping.Model.Models;
using Shoping.Model.Types.Vote;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface IOrderService 
    {
        Task PlaceOrder(int userId);
    }
}
