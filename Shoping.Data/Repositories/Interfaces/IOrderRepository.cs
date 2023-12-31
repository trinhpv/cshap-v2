﻿using Shopping.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Data.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<OrderEntity>
    {
        Task<int> AddAndReturnValue(OrderEntity order);
    }
}
