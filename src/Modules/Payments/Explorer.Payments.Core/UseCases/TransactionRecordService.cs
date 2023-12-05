using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TransactionRecordService : CrudService<TransactionRecordResponseDto, TransactionRecord>, ITransactionRecordService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRecordRepository _repository;

        public TransactionRecordService(ICrudRepository<TransactionRecord> repository, IMapper mapper, ITransactionRecordRepository transactionRecordRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = transactionRecordRepository;
        }

        public Result<PagedResult<TransactionRecordResponseDto>> GetPagedTransactionsByTouristId(int page, int pageSize, long touristId)
        {
            var pagedReviews = _repository.GetPagedTransactionsByTourist(page, pageSize, touristId);
            var result = MapToDto<TransactionRecordResponseDto>(pagedReviews);
            return result;
        }
    }
}
