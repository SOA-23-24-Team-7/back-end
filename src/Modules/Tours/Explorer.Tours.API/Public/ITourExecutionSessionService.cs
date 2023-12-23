using Explorer.Tours.API.Dtos;
using FluentResults;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourExecutionSessionService
    {
        Result<TourExecutionSessionResponseDto> StartTour(long tourId, bool isCampaign, long touristId);
        Result<TourExecutionSessionResponseDto> AbandonTour(long tourId, bool isCampaign, long touristId);
        Result<TourExecutionSessionResponseDto> CheckKeyPointCompletion(long tourId, long touristId, double longitude, double latitude, bool isCampaign);
        Result<List<TourExecutionInfoDto>> GetAllFor(long touristId);
        Result<TourExecutionSessionResponseDto> GetLive(long touristId);
        Result<List<TourExecutionSessionResponseDto>> GetAll();
        Result<List<TourExecutionSessionResponseDto>> GetByTourId(long tourId);
        Result<List<TourExecutionSessionResponseDto>> GetByTourAndTouristId(long tourId, long touristId);
        List<long> GetTouristsIds();
        Result<TourExecutionSessionResponseDto> GetMaximumPorgressExecutionsForTourists(long tourId, long touristId);

    }
}
