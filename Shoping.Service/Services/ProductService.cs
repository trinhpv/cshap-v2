using AutoMapper;
using Shoping.Model.Models;
using Shoping.Model.Types.Category;
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

namespace Shopping.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public void Create(Product product)
        {
            var input = _mapper.Map<ProductEntity>(product);
            _productRepository.Insert(input);
            return;
        }
        public void Update(Product product, int id)
        {
            var currData = _productRepository.GetById(id);
            if (currData == null)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong product id" };
                throw new HttpResponseException(err);
            }
            currData.Price = product.Price;
            currData.Name = product.Name;
            currData.Description = product.Description;
            currData.FirstImage = product.FirstImage;
            currData.SecondImage = product.SecondImage;
            currData.ThirdImage = product.ThirdImage;
            currData.CategoryId = product.CategoryId;
            currData.LastImage = product.LastImage;

            _productRepository.Update(currData);
            return;
        }
        public void Delete(int id)
        {
            _productRepository.Delete(id);
            return;
        }

        public async Task<GetListProductsWithPaginateResponse> GetList(PaginationFilter paginateData)
        {
            // add search string

            Expression<Func<ProductEntity, bool>> filter;
            
            if(!string.IsNullOrWhiteSpace(paginateData.SearchString))
            {
                filter = paginateData.SearchBy switch
                {
                    "Name" => e => e.Name.Contains(paginateData.SearchString),
                    "CategoryId" => e => e.CategoryId == int.Parse(paginateData.SearchString),
                    "LessThanPrice" => e => e.Price <= int.Parse(paginateData.SearchString),
                    "GreaterThanPrice" => e => e.Price >= int.Parse(paginateData.SearchString),
                    _ => e => e.Id != null,
                };
            }
            else
            {
                filter = null;
            }
            

            // add order 
            var orderInfo = paginateData.OrderBy + "_" + paginateData.OrderType.ToString();
            Func<IQueryable<ProductEntity>, IOrderedQueryable<ProductEntity>> orderBy = orderInfo switch
            {
                "Name_Descending" => e => e.OrderByDescending(e => e.Name),
                "Name_Ascending" => e => e.OrderBy(e => e.Name),
                "CreatedDate_Ascending" => e => e.OrderBy(e => e.CreatedDate),
                "CreatedDate_Descending" => e => e.OrderByDescending(e => e.CreatedDate),
                "CategoryId_Ascending" => e => e.OrderBy(e => e.CategoryId),
                "CategoryId_Descending" => e => e.OrderByDescending(e => e.CategoryId),
                "Price_Ascending" => e => e.OrderBy(e => e.Price),
                "Price_Descending" => e => e.OrderByDescending(e => e.Price),
                _ => e => e.OrderBy(e => e.Id),
            };

            var data = await _productRepository.Get(filter, orderBy, includeProperties: "CreatedBy,Category", paginateData);
            var totalCount = await _productRepository.Count(filter);
            var rs = new GetListProductsWithPaginateResponse
            {
                Data = _mapper.Map<IEnumerable<ProductResponseItem>>(data),
                TotalCount = totalCount
            };
            return rs;
        }

        public async Task<Product> Detail(int id)
        {
            var currProduct=  await _productRepository.GetByIdAsync(id);

            if (currProduct == null)
            {

                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong product id" };
                throw new HttpResponseException(err);
            }

            var result = _mapper.Map<Product>(currProduct);
            return result;
        }
    }
}
