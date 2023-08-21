using AutoMapper;
using Shoping.Model.Models;
using Shopping.Data.Repositories.Interfaces;
using Shopping.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IOrderProductRepository orderProductRepository,ICartRepository cartRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task PlaceOrder(int userId)
        {
            Console.WriteLine(userId);
            return;
        }
    }
}
