using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System.Reflection.Metadata.Ecma335;

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

    public Result<PagedResult<TourResponseDto>> Search(TourSearchFilterDto tourSearchFilterDto, SortOption sortBy, bool publishedOnly, Func<long, double?> getDiscount)
    {
        try
        {
            var tours = _tourCrudRepository.GetAll(t => true, include: "KeyPoints,Reviews");

            if (publishedOnly) tours = tours.FindAll(t => t.Status == Domain.Tours.TourStatus.Published);

            var filtered = tours;
            filtered = searchByName(filtered, tourSearchFilterDto.Name);
            filtered = searchByPrice(filtered, tourSearchFilterDto.MinPrice, tourSearchFilterDto.MaxPrice);
            filtered = filterAndSortByDiscount(filtered, tourSearchFilterDto.OnDiscount, getDiscount);
            filtered = searchByDifficulty(filtered, tourSearchFilterDto.MinDifficulty, tourSearchFilterDto.MaxDifficulty);
            filtered = searchByAverageRating(filtered, tourSearchFilterDto.MinAverageRating);
            filtered = searchByAuthorId(filtered, tourSearchFilterDto.AuthorId);
            filtered = searchByLocation(filtered, tourSearchFilterDto.Longitude, tourSearchFilterDto.Latitude, tourSearchFilterDto.MaxDistance);
            filtered = searchByLength(filtered, tourSearchFilterDto.MinLength, tourSearchFilterDto.MaxLength);

            filtered = sort(filtered, sortBy, getDiscount);

            var count = filtered.Count();

            filtered = pageResults(filtered, tourSearchFilterDto.Page, tourSearchFilterDto.PageSize);

            var mappedResult = MapToResponseDto(filtered);

            return new PagedResult<TourResponseDto>(mappedResult, count);
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
            if (page == 0 && pageSize == 0)
            {
                return tours;
            }

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

    private List<Tour> sort(List<Tour> tours, SortOption sortBy, Func<long, double?> getDiscount)
    {
        switch (sortBy) {
            case SortOption.DiscountAsc:
                if (getDiscount == null)
                {
                    return tours;
                }
                return tours.OrderBy(t => getDiscount(t.Id)).ToList();
            case SortOption.DiscountDesc:
                if (getDiscount == null)
                {
                    return tours;
                }
                return tours.OrderByDescending(t => getDiscount(t.Id)).ToList();
            default:
                return tours;
        }
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
            filtered = filtered.FindAll(t => t.Name.ToLower().Contains(name.ToLower()));
        }
        return filtered;
    }

    private List<Tour> searchByLength(List<Tour> tours, double? minLength, double? maxLength)
    {
        var filtered = tours;
        if(minLength!=null && maxLength!=null)
        {
            filtered = filtered.FindAll(t => t.CalculateLength() >= minLength * 1000 && t.CalculateLength() <= maxLength * 1000);
        }
        else if (minLength != null)
        {
            filtered = filtered.FindAll(t => t.CalculateLength() >= minLength * 1000);
        }
        else if (maxLength != null)
        {
            filtered = filtered.FindAll(t => t.CalculateLength() <= maxLength * 1000);
        }
        return filtered;
    }

    private List<Tour> searchByPrice(List<Tour> tours, double? minPrice, double? maxPrice)
    {
        var filtered = tours;
        if(minPrice!=null && maxPrice != null)
        {

            filtered = filtered.FindAll(t => t.Price >= minPrice && t.Price <= maxPrice);
        }
        if (minPrice != null)
        {
            filtered = filtered.FindAll(t => t.Price >= minPrice);
        }
        else if (maxPrice != null)
        {
            filtered = filtered.FindAll(t => t.Price <= maxPrice);
        }
        
        return filtered;
    }

    private List<Tour> filterAndSortByDiscount(List<Tour> tours, bool shouldFilter, Func<long, double?> getDiscount)
    {
        var filtered = tours;
        if (!shouldFilter || getDiscount == null) return filtered;
        filtered = filtered.FindAll(t => getDiscount(t.Id) != null);
        return filtered;
    }

    private List<Tour> searchByDifficulty(List<Tour> tours, int? minDifficulty, int? maxDifficulty)
    {
        var filtered = tours;
        if (minDifficulty != null)
        {
            filtered = filtered.FindAll(t => t.Difficulty >= minDifficulty);
        }
        if (minDifficulty != null)
        {
            filtered = filtered.FindAll(t => t.Difficulty <= maxDifficulty);
        }
        return filtered;
    }

    private List<Tour> searchByAverageRating(List<Tour> tours, double? minAverageRating)
    {
        var filtered = tours;
        if (minAverageRating != null)
        {
            filtered = filtered.FindAll(t => t.GetAverageRating() >= minAverageRating);
        }
        return filtered;
    }

    private List<Tour> searchByAuthorId(List<Tour> tours, int? authorId)
    {
        var filtered = tours;
        if (authorId != null)
        {
            filtered = filtered.FindAll(t => t.AuthorId == authorId);
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
