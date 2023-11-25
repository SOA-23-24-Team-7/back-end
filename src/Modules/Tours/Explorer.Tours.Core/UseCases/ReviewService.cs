using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
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
        private readonly IInternalUserService _internalUserService;
        public ReviewService(ICrudRepository<Review> repository, IReviewRepository reviewRepository, IMapper mapper, IInternalUserService internalUserService) : base(repository, mapper) 
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _internalUserService = internalUserService;
        }

        public Result<PagedResult<ReviewResponseDto>> GetPagedByTourId(int page, int pageSize, long tourId)
        {
            //return MapToDto<ReviewResponseDto>(_reviewRepository.GetPagedByTourId(page, pageSize, tourId));


            var pagedReviews = _reviewRepository.GetPagedByTourId(page, pageSize, tourId);
            var result = MapToDto<ReviewResponseDto>(pagedReviews);
            foreach (var review in result.Value.Results)
            {
                var user = _internalUserService.Get(review.TouristId).Value;
                review.Tourist = user;
            }
            return result;
        }
        public Result<Boolean> ReviewExists(long touristId, long tourId)
        {
            return _reviewRepository.ReviewExists(touristId, tourId);
        }
    }
}
