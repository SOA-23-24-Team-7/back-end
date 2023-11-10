using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases;

public class TourSearchService : BaseService<Tour>, ITourSearchService
{
    private readonly ICrudRepository<Tour> _tourRepository;
    private readonly IKeyPointRepository _keyPointRepository;

    public TourSearchService(ICrudRepository<Tour> tourRepository, IKeyPointRepository keyPointRepository, IMapper mapper) : base(mapper)
    {
        _tourRepository = tourRepository;
        _keyPointRepository = keyPointRepository;
    }

    public Result<PagedResult<TourResponseDto>> Search(double longitude, double latitude, double maxDistance, int page, int pageSize)
    {
        try
        {
            if (maxDistance < 0)
            {
                throw new ArgumentException("Max distance must be greater than 0.");
            }

            var tours = _tourRepository.GetPaged(1, 1000); // ako ima vise od 1000 tura pravice problem
            var nearbyTours = new List<Tour>();

            foreach (var tour in tours.Results)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => k.CalculateDistance(longitude, latitude) <= maxDistance);
                if (nearbyKeypoints.Any())
                {
                    nearbyTours.Add(tour);
                }
            }

            var pagedResult = new PagedResult<Tour>(nearbyTours, nearbyTours.Count);

            return MapToDto<TourResponseDto>(pagedResult);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}
