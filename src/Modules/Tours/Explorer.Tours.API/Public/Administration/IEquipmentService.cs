using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface IEquipmentService
{
    Result<PagedResult<EquipmentResponseDto>> GetPaged(int page, int pageSize);
    Result<EquipmentResponseDto> Create<EquipmentCreateDto>(EquipmentCreateDto equipment);
    Result<EquipmentResponseDto> Update<EquipmentUpdateDto>(EquipmentUpdateDto equipment);
    Result Delete(long id);
}