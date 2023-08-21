using AutoMapper;
using Shoping.Model.Models;
using Shoping.Model.Types.Comment;
using Shopping.Data.Entities;
using Shopping.Data.Repositories.Interfaces;
using Shopping.Service.Interfaces;
using Shopping.Utility.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public void Create(Comment comment)
        {
            var newComment = new CommentEntity
            {
                Content = comment.Content,
                CreatedById = comment.CreatedById,
                ProductId = comment.ProductId,
            };
            _commentRepository.Insert(newComment);
            return;
        }
        public void Update(Comment comment, int id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            _commentRepository.Delete(id);
            return;
        }

        public async Task<GetListCommentsWithPaginateResponse> GetCommentsByProductId(PaginationFilter paginateData, int productId)
        {
            Expression<Func<CommentEntity, bool>> filter = e=> e.ProductId == productId;
            var comments = await _commentRepository.Get(filter, null, includeProperties: "CreatedBy", paginateData);
            var totalCount = await _commentRepository.Count(filter);
            var rs = new GetListCommentsWithPaginateResponse
            {
                Data = _mapper.Map<IEnumerable<SimpleComment>>(comments),
                TotalCount = totalCount
            };
            return rs;
        }
    }
}
