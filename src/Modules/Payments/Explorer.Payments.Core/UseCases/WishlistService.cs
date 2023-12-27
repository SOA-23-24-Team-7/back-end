using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class WishlistService : CrudService<WishlistResponseDto, Wishlist>, IWishlistService
    {
        private readonly ICrudRepository<Wishlist> _repository;

        public WishlistService(ICrudRepository<Wishlist> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public Result<WishlistResponseDto> AddTourToWishlist(WishlistCreateDto wishlist)
        {
            try
            {
                return MapToDto<WishlistResponseDto>(_repository.Create(MapToDomain(wishlist)));
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }

        }

        public void RemoveTourFromWishlist(long wishlistId)
        {
            _repository.Delete(wishlistId);
            return;
        }

        public Result<List<long>> GetTouristToursId(long touristId)
        {
            var wishlist = _repository.GetAll().FindAll(w => w.TouristId == touristId);
            return wishlist.Select(token => token.TourId).ToList();
        }

        public Result RemoveTourFromWishlist(long tourId, long touristId)
        {
            try
            {
                var wishlist = _repository.GetAll().FirstOrDefault(w => w.TourId == tourId && w.TouristId == touristId);
                if (wishlist != null)
                {
                    _repository.Delete(wishlist.Id);
                    return Result.Ok();
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound);
                }

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
    }
}
