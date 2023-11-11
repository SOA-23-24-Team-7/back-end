using Explorer.Tours.API.Dtos.TouristPosition;
using FluentResults;

namespace Explorer.Tours.API.Public.TourExecution;

public interface ITouristPositionService
{
    Result<TouristPositionResponseDto> Create<TouristPositionCreateDto>(TouristPositionCreateDto touristPosition);
    Result<TouristPositionResponseDto> Update<TouristPositionUpdateDto>(TouristPositionUpdateDto touristPosition);
    Result<TouristPositionResponseDto> GetByTouristId(long touristId);
}
