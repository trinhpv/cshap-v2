﻿using Shopping.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public void Create(T obj)
        {
            throw new NotImplementedException();
        }
        public void Update(T obj, int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
