using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ReviewService : CrudService<ReviewResponseDto, Review>, IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(ICrudRepository<Review> repository, IReviewRepository reviewRepository, IMapper mapper) : base(repository, mapper) 
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        public Result<PagedResult<ReviewResponseDto>> GetPagedByTourId(int page, int pageSize, int tourId)
        {
            return MapToDto<ReviewResponseDto>(_reviewRepository.GetPagedByTourId(page, pageSize, tourId));
        }
        public Result<Boolean> ReviewExists(long touristId, int tourId)
        {
            return _reviewRepository.ReviewExists(touristId, tourId);
        }
    }
}
