using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Shoping.Model.Models;
using Shoping.Model.Types.Order;
using Shoping.Model.Types.Product;
using Shopping.Data.Entities;
using Shopping.Data.Repositories.Interfaces;
using Shopping.Data.Repositories.Repositories;
using Shopping.Service.Interfaces;
using Shopping.Utility;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            Expression<Func<CartEntity, bool>> filter = e => e.UserId == userId;
            var cartProducts = await _cartRepository.Get(filter);
            if (!cartProducts.Any())
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Cart is empty" };
                throw new HttpResponseException(err);
            }
            int[] productIds = new int[cartProducts.Count()];
            float totalPay = 0;
            int i = 0;
            foreach (CartEntity cartItem in cartProducts)
            {
                productIds[i] = cartItem.ProductId;
                totalPay += cartItem.TotalPrice;
                i++;
            }
            var order = new OrderEntity
            {
                CreatedById = userId,
                PaymentStatus = Utility.PaymentStatus.Pending,
                ProductIds = productIds,
                TotalPay = totalPay,

            };

            int orderId = await _orderRepository.AddAndReturnValue(order);

            foreach (CartEntity cartItem in cartProducts)
            {

                var orderProduct = new OrderProductEntity
                {
                    ProductId = cartItem.ProductId,
                    UnitPrice = (int)cartItem.UnitPrice,
                    Quantity = cartItem.Quantity,
                    OrderId = orderId,
                };
                _orderProductRepository.Insert(orderProduct);
                _cartRepository.Delete(cartItem.Id);
            }

            return;
        }


        public async Task<IEnumerable<Order>> GetMyOrders(int userId)
        {
            Expression<Func<OrderEntity, bool>> filter = e => e.CreatedById == userId;

            var myOrder = await _orderRepository.Get(filter, null, includeProperties: "OrderProducts.Product", null);

            var rs = _mapper.Map<IEnumerable<Order>>(myOrder);
            return rs;
        }

        public async Task<GetListOrdersWithPaginateResponse> GetOrders(PaginationFilter paginateData)
        {

            Expression<Func<OrderEntity, bool>> filter = e => e.CreatedById == 1;
            // add order 
            var orderInfo = paginateData.OrderBy + "_" + paginateData.OrderType.ToString();
            Func<IQueryable<OrderEntity>, IOrderedQueryable<OrderEntity>> orderBy = orderInfo switch
            {
                "CreatedDate_Ascending" => e => e.OrderBy(e => e.CreatedDate),
                "CreatedDate_Descending" => e => e.OrderByDescending(e => e.CreatedDate),
                "TotalPay_Ascending" => e => e.OrderBy(e => e.TotalPay),
                "TotalPay_Descending" => e => e.OrderByDescending(e => e.TotalPay),
                _ => e => e.OrderBy(e => e.Id),
            };

            var data = await _orderRepository.Get(null, orderBy, includeProperties: "CreatedBy,OrderProducts.Product", paginateData);
            var totalCount = await _orderRepository.Count();
            var rs = new GetListOrdersWithPaginateResponse
            {
                Data = _mapper.Map<IEnumerable<OrderResponseItem>>(data),
                TotalCount = totalCount
            };
            return rs;
        }

        public  void  UpdateStatus(PaymentStatus status, int id)
        {
            var currOrder =  _orderRepository.GetById(id);

            if (currOrder == null)
            {
                var err = new ExType { Status = HttpStatusCode.NotFound, Message = "Not found this order" };
                throw new HttpResponseException(err);
            }

            currOrder.PaymentStatus = status;

            _orderRepository.Update(currOrder);

        }
    }
}
