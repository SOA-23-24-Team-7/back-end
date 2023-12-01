using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class ShoppingNotificationService: CrudService<ShoppingNotificationResponseDto, ShoppingNotification>, IShoppingNotificationService
    {
        private readonly IMapper _mapper;
        private readonly IShoppingNotificationRepository _shoppingNotificationRepository;

        public ShoppingNotificationService(ICrudRepository<ShoppingNotification> repository, IMapper mapper, IShoppingNotificationRepository shoppingNotificationRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _shoppingNotificationRepository = shoppingNotificationRepository;
        }

        public Result<PagedResult<ShoppingNotificationResponseDto>> GetByTouristId(int page, int pageSize, long tourId)
        {
            var pagedReviews = _shoppingNotificationRepository.GetByTouristId(page, pageSize, tourId);
            var result = MapToDto<ShoppingNotificationResponseDto>(pagedReviews);
            return result;
        }

    }
}
