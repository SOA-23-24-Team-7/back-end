using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface INotificationService
    {
        Result<PagedResult<PublicFacilityNotificationResponseDto>> GetFacilityNotificationsByAuthorId(int page, int pageSize,long id);
        Result<PagedResult<PublicKeyPointNotificationResponseDto>> GetKeyPointNotificationsByAuthorId(int page, int pageSize, long id);
        Result<PublicFacilityNotificationResponseDto> Create<NotificationCreateDto>(NotificationCreateDto notification);
    }
}
