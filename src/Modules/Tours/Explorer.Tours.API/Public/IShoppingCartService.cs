using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public
{
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
    }
}
