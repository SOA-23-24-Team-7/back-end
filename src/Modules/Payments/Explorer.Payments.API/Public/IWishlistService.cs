using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IWishlistService
{
    Result<WishlistResponseDto> AddTourToWishlist(WishlistCreateDto wishlist);

}
