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
        Result<TourExecutionSessionResponseDto> StartTour(long tourId, long touristId);
        Result<TourExecutionSessionResponseDto> AbandonTour(long tourId, long touristId);
        Result<TourExecutionSessionResponseDto> CompleteKeyPoint(long tourId, long touristId);
    }
}
