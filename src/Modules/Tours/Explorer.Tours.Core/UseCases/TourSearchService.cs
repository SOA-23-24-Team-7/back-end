using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;

public class TourSearchService : BaseService<Tour>, ITourSearchService
{
    //CHANGED REPOSITORY
    private readonly ITourRepository _tourRepository;
    private readonly ICrudRepository<Tour> _tourCrudRepository;
    private readonly IKeyPointRepository _keyPointRepository;
    private readonly IMapper _mapper;
    private readonly IReviewRepository _reviewRepository;

    public TourSearchService( IKeyPointRepository keyPointRepository, IMapper mapper, ITourRepository tourRepository, IReviewRepository reviewRepository, ICrudRepository<Tour> tourCrudRepository) : base(mapper)
    {
        _tourRepository = tourRepository;
        _keyPointRepository = keyPointRepository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;
        _tourCrudRepository = tourCrudRepository;
    }

    //MOZDA OBRISATI PAGE I PAGESIZE
    public Result<PagedResult<LimitedTourViewResponseDto>> Search(double longitude, double latitude, double maxDistance, int page, int pageSize)
    {
        try
        {
            if (maxDistance < 0)
            {
                throw new ArgumentException("Max distance must be greater than 0.");
            }

            var tours = _tourCrudRepository.GetAll(t => t.Status == Domain.Tours.TourStatus.Published, include: "Reviews"); // ako ima vise od 1000 tura pravice problem
            var nearbyTours = new List<Tour>();

            foreach (var tour in tours)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => k.CalculateDistance(longitude, latitude) <= maxDistance);
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
