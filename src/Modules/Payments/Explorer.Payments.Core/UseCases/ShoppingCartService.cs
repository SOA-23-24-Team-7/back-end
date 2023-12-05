using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class ShoppingCartService : CrudService<ShoppingCartResponseDto, ShoppingCart>, IShoppingCartService
    {
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly ICrudRepository<OrderItem> _orderItemRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly ICrudRepository<ShoppingCart> _repository;
        private readonly ITourService _tourService;
        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IShoppingCartRepository cartRepository, ICrudRepository<OrderItem> orderItemRepository,ICouponRepository couponRepository,ITourService tourService, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
            _couponRepository = couponRepository;
            _tourService = tourService;

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
                if (cart.OrderItems.Any(o => o.TourId == item.TourId))
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Item already exists.");
                var newOrderItem = _orderItemRepository.Create(_mapper.Map<OrderItemCreateDto, OrderItem>(item));
                //cart.AddOrderItem(newOrderItem);
                cart.SetTotalPrice();
                CrudRepository.Update(cart);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result AddBundleOrderItem(BundleOrderItemCreateDto item, long userId)
        {
           // try
            //{
                var cart = _cartRepository.GetByTouristId(userId);
                if (cart.BundleOrderItems.Any(b => b.BundleId == item.BundleId))
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Bundle already exists.");

                var bundleOrderItem = new BundleOrderItem(item.BundleId, item.Price, cart.Id);
                cart.BundleOrderItems.Add(bundleOrderItem);
                cart.SetTotalPrice();
                CrudRepository.Update(cart);

                return Result.Ok();
            /*}
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }*/
        }

        public Result RemoveOrderItem(long id, long shoppingCartId)
        {
            try
            {
                var cart = CrudRepository.Get(shoppingCartId);
                var touristId = cart.TouristId;
                cart = _cartRepository.GetByTouristId(touristId);
                //cart.RemoveOrderItem(id);

                _orderItemRepository.Delete(id);
                cart.SetTotalPrice();
                CrudRepository.Update(cart);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result RemoveBundleOrderItem(long id, long userId)
        {
            try
            {
                var cart = _cartRepository.GetByTouristId(userId);

                var boi = cart.BundleOrderItems.FirstOrDefault(x => x.Id == id);

                if (boi == null) throw new KeyNotFoundException();

                cart.BundleOrderItems.Remove(boi);
                cart.SetTotalPrice();
                CrudRepository.Update(cart);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<OrderItemResponseDto> GetItemByTourId(long tourId, long touristId)
        {
            var cart = _cartRepository.GetByTouristId(touristId);
            OrderItemResponseDto orderItemDto = new OrderItemResponseDto();
            foreach (var item in cart.OrderItems)
            {
                if (item.TourId == tourId)
                {
                    orderItemDto = _mapper.Map<OrderItemResponseDto>(item);
                }
            }
            return orderItemDto;
        }

        public Result<ShoppingCartResponseDto> ApplyCoupon(ApplyCouponRequestDto applyCouponRequestDto)
        {
            try
            {
                
                ShoppingCart cart = _cartRepository.Get(applyCouponRequestDto.ShoppingCartId);
                List<long> tourIds = new List<long>();
                foreach(OrderItem ori in cart.OrderItems)
                {
                    tourIds.Add(ori.TourId);
                }
                var tours = _tourService.GetTours(tourIds).Value;
                
                //find coupon
                Coupon coupon = _couponRepository.FindByCode(applyCouponRequestDto.CouponCode);
                TourResponseDto tour;

                OrderItem oi;
                if (coupon.IsForAllTours())
                {
                    tour = _tourService.Get(coupon.TourId).Value;
                    var authorsTours = tours.FindAll(x => x.AuthorId == tour.AuthorId);
                    tour = authorsTours.FirstOrDefault();
                    //find the most expensive tour
                    foreach (TourResponseDto tourDto in authorsTours)
                    {
                        if(tour.Price < tourDto.Price)
                        {
                            tour = tourDto;
                        }
                    }
                }
                else
                {
                    tour = _tourService.Get(coupon.TourId).Value;
                }
                
                oi = cart.ApplyCouponDiscount(coupon.Discount, tour.Id, tour.Price);
                if (oi == null)
                {
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Coupon already applied.");
                }
                //update the database
                _orderItemRepository.Update(oi);
                _repository.Update(cart);
                return MapToDto<ShoppingCartResponseDto>(cart);

            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


    }
}
