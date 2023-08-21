using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        Task<T?> GetByIdAsync(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", PaginationFilter paginateData = null);
        Task<int> Count(Expression<Func<T, bool>> filter = null);
    }
}
