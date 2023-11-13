using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class TourExecutionSessionService : BaseService<TourExecutionSession>, ITourExecutionSessionService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourExecutionSessionRepository _tourExecutionRepository;
        private readonly IKeyPointRepository _keyPointRepository;

        public TourExecutionSessionService(IMapper mapper, ITourExecutionSessionRepository tourExecutionRepository, IKeyPointRepository keyPointRepository, ITourRepository tourRepository) : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
            _keyPointRepository = keyPointRepository;
            _tourRepository = tourRepository;
        }

        public Result<TourExecutionSessionResponseDto> StartTour(long tourId, long touristId)
        {
            long keyPointId = _keyPointRepository.GetByTourId(tourId)[0].Id;
            TourExecutionSession tourExecution = new TourExecutionSession(tourId, touristId, keyPointId);
            _tourExecutionRepository.Add(tourExecution);
            return MapToDto<TourExecutionSessionResponseDto>(tourExecution);
        }

        public Result<TourExecutionSessionResponseDto> AbandonTour(long tourId, long touristId)
        {
            TourExecutionSession execution = _tourExecutionRepository.Get(tourId, touristId);
            if (execution.Status != Explorer.Tours.Core.Domain.TourExecutionSessionStatus.Started)
            {
                return null;
            }
            execution = _tourExecutionRepository.Abandon(tourId, touristId);
            return MapToDto<TourExecutionSessionResponseDto>(execution);
        }

        public Result<TourExecutionSessionResponseDto> CheckKeyPointCompletion(long tourId, long touristId, double longitude, double latitude)
        {
            TourExecutionSession tourExecution = _tourExecutionRepository.Get(tourId, touristId);
            if(tourExecution.Status != Explorer.Tours.Core.Domain.TourExecutionSessionStatus.Started)
            {
                return null;
            }
            List<KeyPoint> keyPoints = _keyPointRepository.GetByTourId(tourId);
            for (int i = 0; i < keyPoints.Count; i++)
            {
                if (keyPoints[i].Id == tourExecution.NextKeyPointId)
                {
                    TrackProgress(tourExecution, longitude, latitude);

                    if (keyPoints[i].CalculateDistance(longitude, latitude) > 200) break;

                    //ako je kompletirao poslednju kljucnu tacku -> kompletiraj turu
                    if (i + 1 >= keyPoints.Count)
                    {
                        tourExecution = _tourExecutionRepository.CompleteTourExecution(tourExecution.Id);
                    }
                    else
                    {
                        tourExecution = _tourExecutionRepository.UpdateNextKeyPoint(tourExecution.Id, keyPoints[i + 1].Id);
                    }

                    break;
                }
            }
            return MapToDto<TourExecutionSessionResponseDto>(tourExecution);
        }

        private void TrackProgress(TourExecutionSession tourExecutionSession, double longitude, double latitude)
        {
            var tour = _tourRepository.GetById(tourExecutionSession.TourId);

            var nextKeyPoint = _keyPointRepository.Get(tourExecutionSession.NextKeyPointId);
            var previoustKeyPoint = tour.GetPreviousKeyPoint(nextKeyPoint);
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
    }
}
