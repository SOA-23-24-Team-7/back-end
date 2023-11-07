using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ShoppingCartService : CrudService<ShoppingCartResponseDto, ShoppingCart>, IShoppingCartService
    {
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _cartRepository;
        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IShoppingCartRepository cartRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public Result<ShoppingCartResponseDto> GetByTouristId(long id)
        {
            return MapToDto<ShoppingCartResponseDto>(_cartRepository.GetByTouristId(id));
        }

        public Result<ShoppingCartResponseDto> Update(ShoppingCartUpdateDto cart)
        {
            try
            {
                var shoppingCart = _cartRepository.GetByTouristId(cart.TouristId);
                shoppingCart.SetTotalPrice();
                CrudRepository.Update(shoppingCart);
                return MapToDto<ShoppingCartResponseDto>(shoppingCart);
            }
            catch
            {
                return Result.Fail(FailureCode.NotFound);
            }
        }

        //dodajemo
        public bool IsPurchased(long id)
        {
            var cart = CrudRepository.Get(id);
            return cart.IsPurchased;
        }


        

    }
}
