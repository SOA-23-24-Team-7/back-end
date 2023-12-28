using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.Domain.Services
{
    public class TourRecommendersService : BaseService<Tour>, IToursRecommendersService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IPreferenceRepository _tourPreferenceRepository;
        private readonly ITouristPositionRepository _touristPositionRepository;
        private readonly ITourExecutionSessionRepository _tourExecutionRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IInternalTourTokenService _internalTourTokenService;
        public TourRecommendersService(IMapper mapper, IPreferenceRepository preferenceRepository, ITourRepository tourRepository, IKeyPointRepository keyPointRepository, ITouristPositionRepository touristPositionRepository, ITourExecutionSessionRepository tourExecutionRepository, IReviewRepository reviewRepository, IInternalTourTokenService internalTourTokenService) : base(mapper)
        {
            _tourRepository = tourRepository;
            _keyPointRepository = keyPointRepository;
            _mapper = mapper;
            _touristPositionRepository = touristPositionRepository;
            _reviewRepository = reviewRepository;
            _internalTourTokenService = internalTourTokenService;
            _tourPreferenceRepository = preferenceRepository;
            _tourExecutionRepository = tourExecutionRepository;
        }

        public Result<PagedResult<TourResponseDto>> GetActiveTours(long touristId)
        {
            var activeTours = GetActiveToursList(touristId);
            return new PagedResult<TourResponseDto>(activeTours, activeTours.Count);
        }

        public List<TourResponseDto> GetActiveToursList(long touristId)
        {
            List<Tour> nearbyTours = GetNearbyTours(touristId, 100);
            List<double> nearbyToursScores = GetToursHotScores(nearbyTours.Select(tour => tour.Id).ToList());

            List<Tour> topNearbyTours = nearbyTours.Zip(nearbyToursScores)
                .OrderByDescending(tourScore => tourScore.Second)
                .Select(tourScore => tourScore.First)
                .Take(10)
                .ToList();

            for(int i = 0; i < topNearbyTours.Count; i++)
            {
                topNearbyTours[i] = _tourRepository.GetById(topNearbyTours[i].Id);
            }

            var activeTours = MapToResponseDto(topNearbyTours);

            return activeTours;
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
            if (position == null)
            {
                position = new TouristPosition(touristId, 19.8, 45.2);
            }
            var tours = _tourRepository.GetPublishedTours(0, 0);
            List<long> purchasedTours = _internalTourTokenService.GetPurchasedToursIds(touristId);
            List<Tour> nearbyTours = new List<Tour>();
            foreach (var tour in tours.Results)
            {
                var keyPoints = _keyPointRepository.GetByTourId(tour.Id);
                var nearbyKeypoints = keyPoints.Where(k => k.CalculateDistance(position.Longitude, position.Latitude) <= 40000);
                if (nearbyKeypoints.Any() && !purchasedTours.Contains(tour.Id))
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
        public Result<PagedResult<TourResponseDto>> GetRecommendedTours(long touristId)
        {
            List<Tour> nearbyTours = GetNearbyTours(touristId, 10);

            Preference preference = _tourPreferenceRepository.GetByUserId((int)touristId);

            Dictionary<string, int> favouriteTags = GetFinishedTags(touristId, 10);

            List<double> nearbyToursScores = GetToursRecommendScores(nearbyTours.Select(tour => tour.Id).ToList(), preference, favouriteTags);

            List<Tour> topNearbyTours = nearbyTours.Zip(nearbyToursScores)
                .OrderByDescending(tourScore => tourScore.Second)
                .Select(tourScore => tourScore.First)
                .Take(10)
                .ToList();

            var activeTours = MapToResponseDto(topNearbyTours);
            return new PagedResult<TourResponseDto>(activeTours, activeTours.Count);
        }

        public List<TourResponseDto> GetRecommendedToursForMail(long touristId)
        {
            List<Tour> nearbyTours = GetNearbyTours(touristId, 10);

            Preference preference = _tourPreferenceRepository.GetByUserId((int)touristId);

            Dictionary<string, int> favouriteTags = GetFinishedTags(touristId, 10);

            List<double> nearbyToursScores = GetToursRecommendScores(nearbyTours.Select(tour => tour.Id).ToList(), preference, favouriteTags);

            List<Tour> topNearbyTours = nearbyTours.Zip(nearbyToursScores)
                .OrderByDescending(tourScore => tourScore.Second)
                .Select(tourScore => tourScore.First)
                .Take(10)
                .ToList();

            var activeTours = MapToResponseDto(topNearbyTours);
            return activeTours;
        }

        private Dictionary<string, int> GetFinishedTags(long touristId, int v)
        {
            Dictionary<string, int> tags = new Dictionary<string, int>();

            var tourExecutions = _tourExecutionRepository.GetForTourist(touristId);

            int toursCount = 0;
            foreach (TourExecutionSession tourExecution in tourExecutions)
            {
                if (tourExecution.IsCampaign)
                {
                    continue;
                }
                if (tourExecution.Status != TourExecutionSessionStatus.Completed)
                {
                    continue;
                }
                toursCount++;
                var tour = _tourRepository.GetById(tourExecution.TourId);
                foreach (string t in tour.Tags)
                {
                    tags[t] = 0;
                }
                if (toursCount == v)
                {
                    break;
                }
            }
            toursCount = 0;
            foreach (TourExecutionSession tourExecution in tourExecutions)
            {
                if (tourExecution.IsCampaign)
                {
                    continue;
                }
                if (tourExecution.Status != TourExecutionSessionStatus.Completed)
                {
                    continue;
                }
                toursCount++;
                var tour = _tourRepository.GetById(tourExecution.TourId);
                foreach (string t in tour.Tags)
                {
                    tags[t] += 1;
                }
                if (toursCount == v)
                {
                    break;
                }
            }

            return tags;
        }

        private double GetTourRating(long tourId, Preference preference, Dictionary<string, int> favouriteTags)
        {
            double res = 1.0;
            Tour tour = _tourRepository.GetById(tourId);
            if (preference != null)
            {
                //da li se poklapa sa preferiranom tezinom
                if (tour.Difficulty == preference.DifficultyLevel)
                {
                    res *= 1.2;
                }

            }
            foreach (string tagT in tour.Tags)
            {
                if (preference != null)
                {
                    //da li se poklapa sa preferiranim tagovima
                    foreach (string tagP in preference.SelectedTags)
                    {
                        if (tagP.Equals(tagT))
                        {
                            res *= 1.2;
                        }
                    }
                }
                //da li se poklapa sa tagovima iz prethodno zavrsenih do 10 tura
                foreach (string tagF in favouriteTags.Keys)
                {
                    if (tagT.Equals(tagF))
                    {
                        res *= 1.05 * favouriteTags[tagF];
                    }
                }
            }
            long numberOfReviews = _reviewRepository.GetTourReviewCountsAllTime(tourId);
            double averageReviewRating = _reviewRepository.GetTourReviewAverageRatingAllTime(tourId) ?? 0;
            if (averageReviewRating > 4.0 && numberOfReviews > 50)
            {
                res *= 2;
            }
            return res;
        }
        private List<double> GetToursRecommendScores(List<long> tourIds, Preference preference, Dictionary<string, int> favouriteTags)
        {
            List<double> result = new List<double>();
            foreach (var tourId in tourIds)
            {
                double res = GetTourRating(tourId, preference, favouriteTags);
                result.Add(res);
            }
            return result;
        }
    }
}
