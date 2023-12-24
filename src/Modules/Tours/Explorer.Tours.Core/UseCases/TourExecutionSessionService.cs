using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class TourExecutionSessionService : BaseService<TourExecutionSession>, ITourExecutionSessionService, IInternalTourExecutionSessionService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourExecutionSessionRepository _tourExecutionRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        private readonly IInternalEncounterService _encounterService;
        public TourExecutionSessionService(IMapper mapper, ITourExecutionSessionRepository tourExecutionRepository, IKeyPointRepository keyPointRepository, ITourRepository tourRepository, ICampaignRepository campaignRepository, IInternalEncounterService encounterService) : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
            _keyPointRepository = keyPointRepository;
            _tourRepository = tourRepository;
            _mapper = mapper;
            _campaignRepository = campaignRepository;
            _encounterService = encounterService;
        }

        public Result<TourExecutionSessionResponseDto> StartTour(long tourId, bool isCampaign, long touristId)
        {
            long keyPointId;
            if (isCampaign)
                keyPointId = _campaignRepository.GetById(tourId).KeyPointIds[0];
            else
                keyPointId = _keyPointRepository.GetByTourId(tourId)[0].Id;
            TourExecutionSession tourExecution = new TourExecutionSession(tourId, touristId, keyPointId, isCampaign);
            _tourExecutionRepository.Add(tourExecution);
            return MapToDto<TourExecutionSessionResponseDto>(tourExecution);
        }

        public Result<TourExecutionSessionResponseDto> AbandonTour(long tourId, bool isCampaign, long touristId)
        {
            TourExecutionSession execution = _tourExecutionRepository.GetStarted(tourId, isCampaign, touristId);
            if (execution == null)
            {
                return null;
            }
            execution = _tourExecutionRepository.Abandon(execution.Id);
            return MapToDto<TourExecutionSessionResponseDto>(execution);
        }

        public Result<TourExecutionSessionResponseDto> CheckKeyPointCompletion(long tourId, long touristId, double longitude, double latitude, bool isCampaign)
        {
            TourExecutionSession tourExecution = _tourExecutionRepository.GetStarted(tourId, isCampaign, touristId);
            if (tourExecution == null)
            {
                return null;
            }
            if (isCampaign) return CheckCampaignKeyPointCompletion(tourExecution, longitude, latitude);

            return CheckTourKeyPointCompletion(tourExecution, longitude, latitude, touristId);
        }

        private Result<TourExecutionSessionResponseDto> CheckCampaignKeyPointCompletion(TourExecutionSession execution, double longitude, double latitude)
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            Campaign campaign = _campaignRepository.GetById(execution.TourId);
            foreach (var keyPointId in campaign.KeyPointIds)
                keyPoints.Add(_keyPointRepository.Get(keyPointId));

            for (int i = 0; i < keyPoints.Count; i++)
            {
                if (keyPoints[i].Id == execution.NextKeyPointId)
                {

                    if (keyPoints[i].CalculateDistance(longitude, latitude) > 200) break;

                    //ako je kompletirao poslednju kljucnu tacku -> kompletiraj turu
                    if (i + 1 >= keyPoints.Count)
                    {
                        execution = _tourExecutionRepository.CompleteTourExecution(execution.Id);
                    }
                    else
                    {
                        execution = _tourExecutionRepository.UpdateNextKeyPoint(execution.Id, keyPoints[i + 1].Id);
                    }

                    break;
                }
            }
            return MapToDto<TourExecutionSessionResponseDto>(execution);
        }

        private Result<TourExecutionSessionResponseDto> CheckTourKeyPointCompletion(TourExecutionSession execution, double longitude, double latitude, long touristId)
        {
            List<KeyPoint> keyPoints = _keyPointRepository.GetByTourId(execution.TourId);
            TrackProgress(execution, longitude, latitude);
            for (int i = 0; i < keyPoints.Count; i++)
            {
                if (keyPoints[i].Id == execution.NextKeyPointId)
                {

                    if (keyPoints[i].CalculateDistance(longitude, latitude) > 200 || (keyPoints[i].HasEncounter && keyPoints[i].IsEncounterRequired && !_encounterService.IsEncounterInstanceCompleted(touristId, keyPoints[i].Id))) break;

                    //ako je kompletirao poslednju kljucnu tacku -> kompletiraj turu
                    if (i + 1 >= keyPoints.Count)
                    {
                        execution = _tourExecutionRepository.CompleteTourExecution(execution.Id);
                    }
                    else
                    {
                        execution = _tourExecutionRepository.UpdateNextKeyPoint(execution.Id, keyPoints[i + 1].Id);
                    }

                    break;
                }
            }
            return MapToDto<TourExecutionSessionResponseDto>(execution);
        }

        private void TrackProgress(TourExecutionSession tourExecutionSession, double longitude, double latitude)
        {
            var tour = _tourRepository.GetById(tourExecutionSession.TourId);

            var nextKeyPoint = _keyPointRepository.Get(tourExecutionSession.NextKeyPointId);
            var previoustKeyPoint = tour.GetPreviousKeyPoint(nextKeyPoint);
            if (previoustKeyPoint == null) return;

            var nextPreviousDistance = nextKeyPoint.CalculateDistance(previoustKeyPoint);
            var distanceToNext = nextKeyPoint.CalculateDistance(longitude, latitude);

            var currentKeyPoint = tour.KeyPoints.ElementAt(0);
            double distance = 0;
            for (int i = 0; currentKeyPoint != previoustKeyPoint; ++i)
            {
                var next = tour.KeyPoints.ElementAt(i + 1);
                distance += currentKeyPoint.CalculateDistance(next);
                currentKeyPoint = next;
            }

            var relativeDistance = nextPreviousDistance - distanceToNext;
            if (relativeDistance < 0) relativeDistance = 0;

            distance += relativeDistance;
            var length = tour.CalculateLength();
            var percentage = distance / length * 100;

            tourExecutionSession.UpdateProgress(percentage);
            _tourExecutionRepository.Update(tourExecutionSession);
        }

        public Result<List<TourExecutionInfoDto>> GetAllFor(long touristId)
        {
            var tourExecutions = _tourExecutionRepository.GetForTourist(touristId);
            List<TourExecutionInfoDto> tourExecutionInfos = new List<TourExecutionInfoDto>();
            foreach (TourExecutionSession tourExecution in tourExecutions)
            {
                var tour = _tourRepository.GetById(tourExecution.TourId);
                var tourExecutionInfo = this._mapper.Map<TourExecutionInfoDto>(tour);
                this._mapper.Map(tour, tourExecutionInfo);
                tourExecutionInfos.Add(tourExecutionInfo);
            }

            return tourExecutionInfos;
        }
        public Result<TourExecutionSessionResponseDto> GetLive(long touristId)
        {
            var liveTourExecution = _tourExecutionRepository.GetLive(touristId);
            if (liveTourExecution == null)
            {
                return null;
            }
            return MapToDto<TourExecutionSessionResponseDto>(liveTourExecution);
        }

        public Result<List<TourExecutionSessionResponseDto>> GetAll()
        {
            return MapToDto<TourExecutionSessionResponseDto>(_tourExecutionRepository.GetAll());
        }

        public Result<List<TourExecutionSessionResponseDto>>GetByTourId(long tourId)
        {
            return MapToDto<TourExecutionSessionResponseDto>(_tourExecutionRepository.GetAll().Where(tes => tes.TourId == tourId).ToList());
        }

        public Result<List<TourExecutionSessionResponseDto>> GetByTourAndTouristId(long tourId, long touristId)
        {
            List<TourExecutionSession> matchingSessions = _tourExecutionRepository
                                                          .GetAll()
                                                          .Where(tes => tes.TourId == tourId && tes.TouristId == touristId)
                                                          .ToList();

            return MapToDto<TourExecutionSessionResponseDto>(matchingSessions);
        }

        public List<long> GetTouristsIds()
        {
            List<long> uniqueTouristIds = _tourExecutionRepository
                .GetAll()
                .Select(tes => tes.TouristId)
                .Distinct()
                .ToList();

            return uniqueTouristIds;
        }

        public List<long> GetTouristsByTourId(long tourId)
        {
            List<long> uniqueTouristIds = _tourExecutionRepository
                .GetAll()
                .Where(tes => tes.TourId == tourId)
                .Select(tes => tes.TouristId)
                .Distinct()
                .ToList();

            return uniqueTouristIds;
        }

        public Result<TourExecutionSessionResponseDto> GetMaximumPorgressExecutionsForTourists(long tourId, long touristId)
        {
            TourExecutionSessionResponseDto ret = new();
            double maxProgress = 0.0;

            var sessions = GetByTourAndTouristId(tourId, touristId);

            if(sessions.Value.Count > 1)
            {
                foreach (var session in sessions.Value)
                {
                    if (session.Progress > maxProgress)
                    {
                        maxProgress = session.Progress;
                        ret = session;
                    }
                }
            }
            else if (sessions.Value.Count == 1)
            {
               ret = sessions.Value[0];
            }
            else
            {
                return null;
            }
            
            return ret;
        }

    }
}
