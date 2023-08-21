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
    public class VoteRepository : GenericRepository<VoteEntity> , IVoteRepository
    {
        public VoteRepository(ShopingContext _context) : base(_context)
        {
            this.Context = _context;
        }
    }
}
