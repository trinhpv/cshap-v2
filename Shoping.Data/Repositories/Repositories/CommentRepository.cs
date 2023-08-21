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
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(ShopingContext _context) : base(_context)
        {
            this.Context = _context;
        }
    }
}
