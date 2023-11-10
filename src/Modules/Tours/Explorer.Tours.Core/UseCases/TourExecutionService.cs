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
    public class TourExecutionService : BaseService<TourExecution>, ITourExecutionService

    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly IKeyPointRepository _keyPointRepository;

        public TourExecutionService(IMapper mapper, ITourRepository tourRepository, ITourExecutionRepository tourExecutionRepository, IKeyPointRepository keyPointRepository) : base(mapper)
        {
            _tourRepository = tourRepository;
            _tourExecutionRepository = tourExecutionRepository;
            _keyPointRepository = keyPointRepository;
        }
        public Result<TourExecutionResponseDto> StartTour(long tourId, long touristId)
        {
            long keyPointId = _keyPointRepository.GetByTourId(tourId)[0].Id;
            TourExecution tourExecution = new TourExecution(tourId, touristId, keyPointId);
            _tourExecutionRepository.Add(tourExecution);
            return MapToDto<TourExecutionResponseDto>(tourExecution);
        }
        public Result<TourExecutionResponseDto> AbandonTour(long tourId, long touristId)
        {
            TourExecution execution = _tourExecutionRepository.Get(tourId, touristId);
            if (execution.Status != Domain.TourExecutionStatus.Started)
            {
                return null;
            }
            execution = _tourExecutionRepository.Abandon(tourId, touristId);
            return MapToDto<TourExecutionResponseDto>(execution);
        }

        public Result<TourExecutionResponseDto> CompleteKeyPoint(long tourId, long touristId)
        {
            TourExecution tourExecution = _tourExecutionRepository.Get(tourId, touristId);
            if (tourExecution.Status != Domain.TourExecutionStatus.Started)
            {
                return null;
            }
            List<KeyPoint> keyPoints = _keyPointRepository.GetByTourId(tourId);
            for (int i = 0; i < keyPoints.Count; i++)
            {
                if (keyPoints[i].Id == tourExecution.NextKeyPointId)
                {
                    //ako je kompletirao poslednju kljucnu tacku -> kompletiraj turu
                    if (i + 1 >= keyPoints.Count)
                    {
                        tourExecution = _tourExecutionRepository.CompleteTourExecution(tourExecution.Id);
                        break;
                    }
                    else
                    {
                        tourExecution = _tourExecutionRepository.UpdateNextKeyPoint(tourExecution.Id, keyPoints[i + 1].Id);
                        break;
                    }
                }
            }
            return MapToDto<TourExecutionResponseDto>(tourExecution);
        }

        public Result TrackProgress(long tourExecutionId, double longitude, double latitude)
        {
            var tourExecution = _tourExecutionRepository.Get(tourExecutionId);
            var tour = _tourRepository.GetById(tourExecution.TourId);

            var nextKeyPoint = _keyPointRepository.Get(tourExecution.NextKeyPointId);
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

            tourExecution.UpdateProgress(percentage);

            return Result.Ok(); // vratiti percentage
        }
    }
}
