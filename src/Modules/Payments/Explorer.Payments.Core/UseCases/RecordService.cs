using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class RecordService : CrudService<RecordResponseDto, Record>, IRecordService
    {
        private readonly IMapper _mapper;
        private readonly IRecordRepository _repository;
        private IInternalTourService _internalTourService;
        public RecordService(ICrudRepository<Record> repository, IMapper mapper, IRecordRepository recordRepository, IInternalTourService internalTourService) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = recordRepository;
            _internalTourService = internalTourService;
        }

        public Result<PagedResult<RecordResponseDto>> GetPagedByTouristId(int page, int pageSize, long touristId)
        {
            var pagedReviews = _repository.GetPagedByTouristId(page, pageSize, touristId);
            var result = MapToDto<RecordResponseDto>(pagedReviews);
            foreach (var record in result.Value.Results)
            {
                var tour = _internalTourService.Get(record.TourId).Value;
                record.TourName = tour.Name;
            }

            return result;
        }
    }
}
