using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristEquipment;
using FluentResults;

namespace Explorer.Tours.API.Public
{
    public interface IOrderItemService
    {
        Result<PagedResult<OrderItemResponseDto>> GetPaged(int page, int pageSize);
        Result<OrderItemResponseDto> Create<OrderItemCreateDto>(OrderItemCreateDto cart);
        Result Delete(long id);
        Result<OrderItemResponseDto> Update<OrderItemUpdateDto>(OrderItemUpdateDto cart);
    }
}
