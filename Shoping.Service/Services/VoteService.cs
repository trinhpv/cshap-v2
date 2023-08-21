using AutoMapper;
using Shoping.Model.Models;
using Shoping.Model.Types.Comment;
using Shoping.Model.Types.Vote;
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
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;
        public VoteService(IVoteRepository voteRepository, IMapper mapper) { 
        
            _voteRepository = voteRepository;
            _mapper = mapper;
        }
        public void Create(Vote vote)
        {
            throw new NotImplementedException();
        }
        public async Task CreateVote(Vote vote)
        {
            Expression<Func<VoteEntity, bool>> filter = e => e.ProductId == vote.ProductId && e.CreatedById == vote.CreatedById;
            var votedTime = await _voteRepository.Count(filter);
            if (votedTime > 0)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Already voted" };
                throw new HttpResponseException(err);
            }
            var newVote = new VoteEntity
            {
                Id = vote.Id,
                CreatedById = vote.CreatedById,
                ProductId = vote.ProductId,
                Size = vote.Size,
            };
            _voteRepository.Insert(newVote);
            return ;
        }
        public void Update(Vote vote, int id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            _voteRepository.Delete(id);
            return;
        }

        public async Task<GetListVotesWithPaginateResponse> GetVotesByProduct(PaginationFilter paginateData, int productId)
        {
            Expression<Func<VoteEntity, bool>> filter = e => e.ProductId == productId;
            var votes = await _voteRepository.Get(filter, null, includeProperties: "CreatedBy", paginateData);
            var totalCount = await _voteRepository.Count(filter);

            int sumSize = 0;
            foreach ( var vote in votes )
            {
                sumSize = sumSize + vote.Size;
            }
            var rs = new GetListVotesWithPaginateResponse
            {
                Data = _mapper.Map<IEnumerable<SimpleVote>>(votes),
                TotalCount = totalCount,
                AvgVote = (double)sumSize/(double)totalCount,
            };
            return rs;
        }
    }
}
