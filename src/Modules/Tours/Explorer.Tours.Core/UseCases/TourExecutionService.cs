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
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        public TourExecutionService(IMapper mapper, ITourExecutionRepository tourExecutionRepository, IKeyPointRepository keyPointRepository) : base(mapper)
        {
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
    }
}
