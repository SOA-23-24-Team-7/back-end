using System;
using System.Collections.Generic;
using System.Linq;
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
        Result<ShoppingCartResponseDto> Update<ShoppingCartUpdateDto>(ShoppingCartUpdateDto cart);
        Result Delete(long id);
        public bool IsPurchased(long id);
    }
}
