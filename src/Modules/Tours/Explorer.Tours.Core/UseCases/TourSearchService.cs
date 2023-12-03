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
    public Result<PagedResult<LimitedTourViewResponseDto>> SearchByLocation(double longitude, double latitude, double maxDistance, int page, int pageSize)
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

    public Result<PagedResult<TourResponseDto>> Search(TourTouristSearchFilterDto tourSearchFilterDto)
    {
        var filter = new TourAuthorSearchFilterDto(tourSearchFilterDto);
        return Search(filter);
    }

    public Result<PagedResult<TourResponseDto>> Search(TourAuthorSearchFilterDto tourSearchFilterDto)
    {
        try
        {
            var tours = _tourCrudRepository.GetAll(t => true, include: "KeyPoints,Reviews");

            var filtered = tours;
            filtered = searchByName(filtered, tourSearchFilterDto.Name);
            filtered = searchByPrice(filtered, tourSearchFilterDto.MinPrice, tourSearchFilterDto.MaxPrice);
            filtered = searchByDifficulty(filtered, tourSearchFilterDto.MinDifficulty, tourSearchFilterDto.MaxDifficulty);
            filtered = searchByAverageRating(filtered, tourSearchFilterDto.MinAverageRating);
            filtered = searchByAuthorId(filtered, tourSearchFilterDto.AuthorId);
            filtered = searchPublishedTours(filtered, tourSearchFilterDto.TourStatus);
            filtered = searchByLocation(filtered, tourSearchFilterDto.Longitude, tourSearchFilterDto.Latitude, tourSearchFilterDto.MaxDistance);
            filtered = searchByLength(filtered, tourSearchFilterDto.MinLength, tourSearchFilterDto.MaxLength);
            filtered = pageResults(filtered, tourSearchFilterDto.Page, tourSearchFilterDto.PageSize);

            var mappedResult = MapToResponseDto(filtered);

            return new PagedResult<TourResponseDto>(mappedResult, mappedResult.Count);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private List<Tour> pageResults(List<Tour> tours, int? page, int? pageSize)
    {
        if (page != null && pageSize != null)
        {
            int startIndex = (page.Value - 1) * pageSize.Value;

            if (startIndex >= tours.Count)
            {
                return new List<Tour>();
            }

            int endIndex = startIndex + pageSize.Value;

            if (endIndex > tours.Count)
            {
                endIndex = tours.Count;
            }

            var results = tours.GetRange(startIndex, endIndex - startIndex);

            return results;
        }

        return tours;
    }

    private List<Tour> searchByLocation(List<Tour> tours, double? longitude, double? latitude, double? maxDistance)
    {
        if (longitude != null && latitude != null && maxDistance != null)
        {
            if (maxDistance < 0)
            {
                throw new ArgumentException("Max distance must be greater than 0.");
            }

            var nearbyTours = new List<Tour>();


            foreach (var tour in tours)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => k.CalculateDistance(longitude.Value, latitude.Value) <= maxDistance);
                if (nearbyKeypoints.Any())
                {
                    nearbyTours.Add(tour);
                }
            }

            return nearbyTours;
        }

        return tours;
    }

    private List<Tour> searchByName(List<Tour> tours, string? name)
    {
        var filtered = tours;
        if (name != null)
        {
            filtered = tours.FindAll(t => t.Name.ToLower().Contains(name.ToLower()));
        }
        return filtered;
    }

    private List<Tour> searchByLength(List<Tour> tours, double? minLength, double? maxLength)
    {
        var filtered = tours;
        if (minLength != null)
        {
            filtered = tours.FindAll(t => t.CalculateLength() >= minLength);
        }
        if (maxLength != null)
        {
            filtered = tours.FindAll(t => t.CalculateLength() <= maxLength);
        }
        return filtered;
    }

    private List<Tour> searchByPrice(List<Tour> tours, double? minPrice, double? maxPrice)
    {
        var filtered = tours;
        if (minPrice != null)
        {
            filtered = tours.FindAll(t => t.Difficulty >= minPrice);
        }
        if (maxPrice != null)
        {
            filtered = tours.FindAll(t => t.Difficulty <= maxPrice);
        }
        return filtered;
    }

    private List<Tour> searchByDifficulty(List<Tour> tours, int? minDifficulty, int? maxDifficulty)
    {
        var filtered = tours;
        if (minDifficulty != null)
        {
            filtered = tours.FindAll(t => t.Difficulty >= minDifficulty);
        }
        if (minDifficulty != null)
        {
            filtered = tours.FindAll(t => t.Difficulty <= maxDifficulty);
        }
        return filtered;
    }

    private List<Tour> searchByAverageRating(List<Tour> tours, double? minAverageRating)
    {
        var filtered = tours;
        if (minAverageRating != null)
        {
            filtered = tours.FindAll(t => t.GetAverageRating() >= minAverageRating);
        }
        return filtered;
    }

    private List<Tour> searchByAuthorId(List<Tour> tours, int? authorId)
    {
        var filtered = tours;
        if (authorId != null)
        {
            filtered = tours.FindAll(t => t.AuthorId == authorId);
        }
        return filtered;
    }

    private List<Tour> searchPublishedTours(List<Tour> tours, string? isPublished)
    {
        var filtered = tours;
        if (isPublished != null)
        {
            Enum.TryParse(isPublished, out Domain.Tours.TourStatus myStatus);
            filtered = filtered.FindAll(t => t.Status == myStatus);
        }
        return filtered;
    }

    private List<LimitedTourViewResponseDto> MapToLimitedTourViewDto(List<Tour> result)
    {
        List<LimitedTourViewResponseDto> dtos = new List<LimitedTourViewResponseDto>();
            foreach (var tour in result)
            {
                LimitedTourViewResponseDto dto = _mapper.Map<LimitedTourViewResponseDto>(tour);
                dto.KeyPoint = _mapper.Map<KeyPointResponseDto>(tour.KeyPoints.FirstOrDefault());
                var reviews = _reviewRepository.GetPagedByTourId(0, 0, tour.Id);
                dto.Reviews = reviews.Results.Select(_mapper.Map<ReviewResponseDto>).ToList();
               dtos.Add(dto);
            }
        return dtos;
    }

    private List<TourResponseDto> MapToResponseDto(List<Tour> tours)
    {
        List<TourResponseDto> mapped = new List<TourResponseDto>();
        foreach(var tour in tours)
        {
            var dto = _mapper.Map<TourResponseDto>(tour);
            foreach (var keyPoint in tour.KeyPoints)
            {
                dto.KeyPoints.Add(_mapper.Map<KeyPointResponseDto>(keyPoint));
            }
            mapped.Add(_mapper.Map<TourResponseDto>(tour));
        }
        return mapped;
    }
}
