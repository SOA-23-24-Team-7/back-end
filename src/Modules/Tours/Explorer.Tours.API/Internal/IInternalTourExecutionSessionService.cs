using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Internal;

public interface IInternalTourExecutionSessionService
{
    Result<List<TourExecutionSessionResponseDto>> GetByTourAndTouristId(long tourId, long touristId);
    List<long> GetTouristsByTourId(long tourId);

}
