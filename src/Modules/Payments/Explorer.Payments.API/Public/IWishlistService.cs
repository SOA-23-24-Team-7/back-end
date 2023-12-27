using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IWishlistService
{
    Result<WishlistResponseDto> AddTourToWishlist(WishlistCreateDto wishlist);
    Result<List<long>> GetTouristToursId(long touristId);
    Result RemoveTourFromWishlist(long tourId, long touristId);
}
