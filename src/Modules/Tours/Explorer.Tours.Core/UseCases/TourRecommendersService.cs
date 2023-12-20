using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class TourRecommendersService : BaseService<Tour>, IToursRecommendersService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITouristPositionRepository _touristPositionRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IInternalTourTokenService _internalTourTokenService;
        public TourRecommendersService(IMapper mapper, ITourRepository tourRepository, IKeyPointRepository keyPointRepository, ITouristPositionRepository touristPositionRepository, IReviewRepository reviewRepository, IInternalTourTokenService internalTourTokenService) : base (mapper)
        {
            _tourRepository = tourRepository;
            _keyPointRepository = keyPointRepository;
            _mapper = mapper;
            _touristPositionRepository = touristPositionRepository;
            _reviewRepository = reviewRepository;
            _internalTourTokenService = internalTourTokenService;
        }

        public Result<PagedResult<TourResponseDto>> GetActiveTours(long touristId)
        {
            List<Tour> nearbyTours = GetNearbyTours(touristId, 100);
            List<double> nearbyToursScores = GetToursHotScores(nearbyTours.Select(tour => tour.Id).ToList());

            List<Tour> topNearbyTours = nearbyTours.Zip(nearbyToursScores)
                .OrderByDescending(tourScore => tourScore.Second)
                .Select(tourScore => tourScore.First)
                .Take(10)
                .ToList();

            var activeTours = MapToResponseDto(topNearbyTours);
            return new PagedResult<TourResponseDto>(activeTours, activeTours.Count);
        }

        public Result<PagedResult<TourResponseDto>> GetRecommendedTours(long touristId)
        {
            List<Tour> nearbyTours = GetNearbyTours(touristId, 10);
            var activeTours = MapToResponseDto(nearbyTours);
            return new PagedResult<TourResponseDto>(activeTours, activeTours.Count);
        }

        private List<TourResponseDto> MapToResponseDto(List<Tour> tours)
        {
            List<TourResponseDto> mapped = new List<TourResponseDto>();
            foreach (var tour in tours)
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

        private List<Tour> GetNearbyTours(long touristId, int numberOfToursToGet)
        {
            TouristPosition position = _touristPositionRepository.GetByTouristId(touristId);
            var tours = _tourRepository.GetPublishedTours(0, 0);
            List<Tour> nearbyTours = new List<Tour>();
            foreach (var tour in tours.Results)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => k.CalculateDistance(position.Longitude, position.Latitude) <= 40000);
                if (nearbyKeypoints.Any())
                {
                    nearbyTours.Add(tour);
                }
                if (nearbyTours.Count > numberOfToursToGet)
                {
                    break;
                }
            }
            return nearbyTours;
        }

        private List<double> GetToursHotScores(List<long> tourIds)
        {
            List<double> result = new List<double>();
            foreach (var tourId in tourIds)
            {
                TourHotInfo tourHotInfo = GetTourHotInfo(tourId);
                result.Add(tourHotInfo.GetScore());
            }
            return result;
        }

        private TourHotInfo GetTourHotInfo(long tourId)
        {
            long numberOfReviews = _reviewRepository.GetTourReviewCounts(tourId, 10);
            double averageReviewRating = _reviewRepository.GetTourReviewAverageRating(tourId, 10) ?? 0;
            long numberOfPurchases = _internalTourTokenService.GetTourTokenCount(tourId);
            TourHotInfo ret = new TourHotInfo(numberOfReviews, averageReviewRating, numberOfPurchases);
            return ret;
        }

    }
}
