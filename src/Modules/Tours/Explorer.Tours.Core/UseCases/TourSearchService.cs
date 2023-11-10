using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;

public class TourSearchService : BaseService<Tour>, ITourSearchService
{
    //CHANGED REPOSITORY
    private readonly ITourRepository _tourRepository;
    private readonly IKeyPointRepository _keyPointRepository;
    private readonly IMapper _mapper;
    private readonly IReviewRepository _reviewRepository;

    public TourSearchService( IKeyPointRepository keyPointRepository, IMapper mapper, ITourRepository tourRepository, IReviewRepository reviewRepository) : base(mapper)
    {
        _tourRepository = tourRepository;
        _keyPointRepository = keyPointRepository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;

    }

    public Result<PagedResult<LimitedTourViewResponseDto>> Search(double longitude, double latitude, double maxDistance, int page, int pageSize)
    {
        try
        {
            if (maxDistance < 0)
            {
                throw new ArgumentException("Max distance must be greater than 0.");
            }

            Coordinate mapCoordinate = new Coordinate(longitude, latitude);
            // CHANGE TO PUBLISHED TOURS
            var tours = _tourRepository.GetPublishedTours(1, 1000); // ako ima vise od 1000 tura pravice problem
            var nearbyTours = new List<Tour>();

            foreach (var tour in tours.Results)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => mapCoordinate.CalculateDistance(k.Longitude, k.Latitude) <= maxDistance);
                if (nearbyKeypoints.Any())
                {
                    nearbyTours.Add(tour);
                }
            }

            var mappedResult = MapToLimitedTourViewDto(nearbyTours);
            return new PagedResult<LimitedTourViewResponseDto>(mappedResult, mappedResult.Count);

            
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private List<LimitedTourViewResponseDto> MapToLimitedTourViewDto(List<Tour> result)
    {
        List<LimitedTourViewResponseDto> dtos = new List<LimitedTourViewResponseDto>();
            foreach (var tour in result)
            {
                LimitedTourViewResponseDto dto = _mapper.Map<LimitedTourViewResponseDto>(tour);
                dto.KeyPoint = _mapper.Map<KeyPointResponseDto>(tour.KeyPoints.First());
                var reviews = _reviewRepository.GetPagedByTourId(0, 0, tour.Id);
                dto.Reviews = reviews.Results.Select(_mapper.Map<ReviewResponseDto>).ToList();
               dtos.Add(dto);
            }
        return dtos;
    }
}
