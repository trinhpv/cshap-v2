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
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(ShopingContext _context) : base(_context)
        {
            this.Context = _context;
        }

        public async  override Task<ProductEntity?> GetByIdAsync(int id)
        {
            IQueryable<ProductEntity> query = Table;
            query = query.Include(e => e.Category);
            query = query.Include(e => e.CreatedBy);
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
