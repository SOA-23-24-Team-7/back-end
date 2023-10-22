using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.TouristEquipment;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface ITouristEquipmentService
    {
        Result<PagedResult<TouristEquipmentResponseDto>> GetPaged(int page, int pageSize);
        Result<TouristEquipmentResponseDto> Create<TouristEquipmentCreateDto>(TouristEquipmentCreateDto touristEquipment);
        Result<TouristEquipmentResponseDto> Update<TouristEquipmentUpdateDto>(TouristEquipmentUpdateDto touristEquipment);
        Result Delete(long Id);
    }
}
