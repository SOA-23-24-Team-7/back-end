using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface IShoppingNotificationService
    {
        Result<PagedResult<ShoppingNotificationResponseDto>> GetByTouristId(int page, int pageSize, long id);
        Result<ShoppingNotificationResponseDto> Create<ShoppingNotificationCreateDto>(ShoppingNotificationCreateDto notification);
        Result<ShoppingNotificationResponseDto> SetSeenStatus(long id);
        Result<int> CountUnseenNotifications(long userId);
    }
}
