using AutoMapper;
using Shoping.Model.Models;
using Shopping.Data.Entities;
using Shopping.Data.Repositories.Interfaces;
using Shopping.Service.Interfaces;
using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CartService(ICartRepository cateRepository, IProductRepository productRepository, IMapper mapper)
        {

            _cartRepository = cateRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public void Create(Cart cart)
        {
            throw new NotImplementedException();
        }
        public async Task Add(Cart cart)
        {
            Expression<Func<CartEntity, bool>> filter = e=> e.UserId == cart.UserId && e.ProductId == cart.ProductId;
            var currentInCart = await _cartRepository.Get(filter);

            var product = await _productRepository.GetByIdAsync(cart.ProductId);

            if(product ==null)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong product id" };
                throw new HttpResponseException(err);
            }

            if(currentInCart == null || !currentInCart.Any())
            {

                var cartData = new CartEntity
                {
                    ProductId = cart.ProductId,
                    UserId = cart.UserId,
                    Quantity = cart.Quantity,
                    TotalPrice = (float)cart.Quantity * (float)product.Price,
                    UnitPrice = product.Price,
                };
                _cartRepository.Insert(cartData);
            }
            else
            {
                var available = currentInCart.First();
                available.Quantity += cart.Quantity;
                available.TotalPrice += ((float)cart.Quantity * (float)product.Price);
                available.UnitPrice = product.Price;
                _cartRepository.Update(available);
            }
            return;
        }
        public void Update(Cart cart, int id)
        {
            throw new NotImplementedException();
        }
        public void Delete( int id)
        {
            _cartRepository.Delete(id);
            return;
        }

        public async Task<IEnumerable<Cart>> GetMyCart(int userId)
        {
            Expression<Func<CartEntity, bool>> filter = e => e.UserId == userId ;

            var list = await _cartRepository.Get(filter, null, includeProperties: "Product");
            var rs = _mapper.Map<IEnumerable<Cart>>(list);
            return rs;
        }
    }
}
