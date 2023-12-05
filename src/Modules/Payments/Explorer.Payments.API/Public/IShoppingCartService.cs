using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IShoppingCartService
{
    Result<ShoppingCartResponseDto> GetByTouristId(long id);
    Result<ShoppingCartResponseDto> Create<ShoppingCartCreateDto>(ShoppingCartCreateDto cart);
    Result<ShoppingCartResponseDto> Update(ShoppingCartUpdateDto cart);
    Result Delete(long id);
    public bool IsPurchased(long id);
    Result AddOrderItem(OrderItemCreateDto item);
    Result RemoveOrderItem(long id, long shoppingCartId);
    Result<OrderItemResponseDto> GetItemByTourId(long tourId, long touristId);
    public Result<ShoppingCartResponseDto> ApplyCoupon(ApplyCouponRequestDto applyCouponRequestDto);
}
