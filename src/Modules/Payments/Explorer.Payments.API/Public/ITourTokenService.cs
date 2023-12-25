using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface ITourTokenService
{
    Result<TourTokenResponseDto> AddToken(TourTokenCreateDto token, long totalPrice, long orderItemPrice);
    Result<List<TourTokenResponseDto>> GetTouristsTokens(long touristId);
    Result<List<long>> GetTouristToursId(long touristId);
    public Result AddTokensByBundle(long touristId, long bundleId);
    Result<List<TourTokenResponseDto>> GetAll();
}
