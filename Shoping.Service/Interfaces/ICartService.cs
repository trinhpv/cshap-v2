using Shoping.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface ICartService: IBaseService<Cart>
    {
        Task Add(Cart cart);
        Task<IEnumerable<Cart>> GetMyCart(int userId);
    }
}
