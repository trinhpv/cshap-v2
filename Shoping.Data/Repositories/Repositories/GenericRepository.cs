using Microsoft.EntityFrameworkCore;
using Shopping.Data.DataContext;
using Shopping.Data.Repositories.Interfaces;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Data.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ShopingContext Context = null;
        protected DbSet<T> Table = null;


        public GenericRepository(ShopingContext _context)
        {
            this.Context = _context;
            Table = _context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Table.ToList();
        }

        public virtual T GetById(int id)
        {
            return Table.Find(id);
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }
        public virtual void Insert(T obj)
        {
            Table.Add(obj);
            Context.SaveChanges();
        }
        public virtual void Update(T obj)
        {
            Table.Update(obj);
            Context.SaveChanges();
        }
        public virtual void Delete(int id)
        {
            T existing = Table.Find(id);
            Table.Remove(existing);
            Context.SaveChanges();
        }

        public async virtual Task<int> Count(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }
        public async virtual Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", PaginationFilter paginateData = null)
        {
            IQueryable<T> query = Table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(paginateData != null)
            {
                if (orderBy != null)
                {
                    return await orderBy(query).Skip((paginateData.PageNumber -1)* paginateData.PageSize).Take(paginateData.PageSize).ToListAsync();
                }
                else
                {
                    return await query.Skip((paginateData.PageNumber - 1) * paginateData.PageSize).Take(paginateData.PageSize).ToListAsync();
                }
            }
            else
            {
                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            
        }

        


    }
}
