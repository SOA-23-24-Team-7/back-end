using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public;

public interface IOrderItemService
{
    Result<PagedResult<OrderItemResponseDto>> GetPaged(int page, int pageSize);
    Result<OrderItemResponseDto> Create<OrderItemCreateDto>(OrderItemCreateDto cart);
    Result Delete(long id);
    Result<OrderItemResponseDto> Update<OrderItemUpdateDto>(OrderItemUpdateDto cart);
}
