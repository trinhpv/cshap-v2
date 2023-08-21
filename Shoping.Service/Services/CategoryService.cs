using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.ObjectPool;
using Shoping.Model.Models;
using Shoping.Model.Types.Category;
using Shopping.Data.Entities;
using Shopping.Data.Repositories.Interfaces;
using Shopping.Service.Interfaces;
using Shopping.Utility;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public void Create(Category cate)
        {
            var input = _mapper.Map<CategoryEntity>(cate);
            _categoryRepository.Insert(input);
            return;
        }
        public void Update(Category cate, int id)
        {
            var currData = _categoryRepository.GetById(id);
            if (currData == null)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong category id" };
                throw new HttpResponseException(err);
            }
            currData.Name = cate.Name;
            currData.Description = cate.Description;
            _categoryRepository.Update(currData);
            return;
        }
        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
            return;
        }
        public GetCategoryResponse Detail(int id)
        {
            var cate = _categoryRepository.GetById(id);
            if (cate == null)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong category id" };
                throw new HttpResponseException(err);
            }
            var result = new GetCategoryResponse
            {
                Id = cate.Id,
                Name = cate.Name,
                Description = cate.Description,
                CreatedDate = cate.CreatedDate,
                CreatedBy = new SimpleUser
                {
                    Id = cate.CreatedBy.Id,
                    UserName = cate.CreatedBy.UserName,
                }
            };
            return result;
        }
        public async Task<GetListCateWithPaginateResponse> GetList(PaginationFilter paginateData)
        {
            // add search string
            Expression<Func<CategoryEntity, bool>> filter = e => e.Name.Contains(paginateData.SearchString);

            //add order 
            var orderInfo = paginateData.OrderBy+ "_" + paginateData.OrderType.ToString();
            Func<IQueryable<CategoryEntity>, IOrderedQueryable<CategoryEntity>> orderBy = orderInfo switch
            {
                "Name_Descending" => e => e.OrderByDescending(e => e.Name),
                "Name_Ascending" => e => e.OrderBy(e => e.Name),
                "CreatedDate_Ascending" => e => e.OrderBy(e => e.CreatedDate),
                "CreatedDate_Descending" => e => e.OrderByDescending(e => e.CreatedDate),
                _ => e => e.OrderBy(e => e.Id),
            };
            var data = await _categoryRepository.Get(filter, orderBy, includeProperties: "CreatedBy", paginateData);
            var totalCount = await _categoryRepository.Count(filter);
            var rs = new GetListCateWithPaginateResponse
            {
                Data = _mapper.Map<IEnumerable<GetListCategoryResponse>>(data),
                TotalCount = totalCount,
            };
            return rs;
        }

       
        
    }
}
