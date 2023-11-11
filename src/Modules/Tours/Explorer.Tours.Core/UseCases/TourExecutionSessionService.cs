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
        private readonly ITourExecutionSessionRepository _tourExecutionRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        public TourExecutionSessionService(IMapper mapper, ITourExecutionSessionRepository tourExecutionRepository, IKeyPointRepository keyPointRepository) : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
            _keyPointRepository = keyPointRepository;
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
            if (execution.Status != TourExecutionSessionStatus.Started)
            {
                return null;
            }
            execution = _tourExecutionRepository.Abandon(tourId, touristId);
            return MapToDto<TourExecutionSessionResponseDto>(execution);
        }

        public Result<TourExecutionSessionResponseDto> CompleteKeyPoint(long tourId, long touristId)
        {
            TourExecutionSession tourExecution = _tourExecutionRepository.Get(tourId, touristId);
            if(tourExecution.Status != TourExecutionSessionStatus.Started)
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
            return MapToDto<TourExecutionSessionResponseDto>(tourExecution);
        }
    }
}
