using Microsoft.EntityFrameworkCore;
using Shopping.Data.DataContext;
using Shopping.Data.Entities;
using Shopping.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Data.Repositories.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ShopingContext _context) : base(_context)
        {
            this.Context = _context;
        }
        public override IEnumerable<CategoryEntity> GetAll()
        {
            var listCategory = Table.Include(f => f.CreatedBy).ToList();
            return listCategory;

        }
        public override CategoryEntity GetById(int id)
        {
            IQueryable<CategoryEntity> query = Table;
            query = query.Include(e => e.CreatedBy);
            return query.First(x => x.Id == id);
        }
    }
}
