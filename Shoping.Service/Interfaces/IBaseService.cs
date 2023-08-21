using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        void Create(T obj);
        void Update(T obj, int id);
        void Delete(int id);
         //IEnumerable<T> GetList();

    }
}
