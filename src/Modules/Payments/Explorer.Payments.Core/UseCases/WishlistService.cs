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
    }
}
