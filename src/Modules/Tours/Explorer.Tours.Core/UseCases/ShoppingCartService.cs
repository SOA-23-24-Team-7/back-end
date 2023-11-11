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
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class ShoppingCartService : CrudService<ShoppingCartResponseDto, ShoppingCart>, IShoppingCartService
    {
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly ICrudRepository<OrderItem> _orderItemRepository;
        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IShoppingCartRepository cartRepository, ICrudRepository<OrderItem> orderItemRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
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

        public Result AddOrderItem(OrderItemCreateDto item)
        {
            try
            {
                var cart = CrudRepository.Get(item.ShoppingCartId);
                var touristId = cart.TouristId;
                cart = _cartRepository.GetByTouristId(touristId);
                if(cart.OrderItems.Any(o => o.TourId == item.TourId))
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Item already exists.");
                var newOrderItem = _orderItemRepository.Create(_mapper.Map<OrderItemCreateDto, OrderItem>(item));
                cart.AddOrderItem(newOrderItem);
                CrudRepository.Update(cart);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result RemoveOrderItem(long id, long shoppingCartId)
        {
            try
            {
                var cart = CrudRepository.Get(shoppingCartId);
                var touristId = cart.TouristId;
                cart = _cartRepository.GetByTouristId(touristId);
                cart.RemoveOrderItem(id);
                
                _orderItemRepository.Delete(id);
                CrudRepository.Update(cart);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

    }
}
