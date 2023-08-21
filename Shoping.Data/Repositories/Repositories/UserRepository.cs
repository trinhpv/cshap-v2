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
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {

        public UserRepository(ShopingContext context) : base(context)
        {
            this.Context = context;
        }

        public async Task<UserEntity> FindByUserName(string userName)
        {
            var user = await Table.FirstAsync<UserEntity>(x => x.UserName == userName);
            return user;
        }
        public UserEntity? FindUserName(string userName)
        {
            return Table.FirstOrDefault(x => x.UserName == userName);
        }

    }
}
