using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface ITourExecutionService
    {
        Result<TourExecutionResponseDto> StartTour(long tourId, long touristId);
        Result<TourExecutionResponseDto> AbandonTour(long tourId, long touristId);
        Result<TourExecutionResponseDto> CompleteKeyPoint(long tourId, long touristId);
    }
}
