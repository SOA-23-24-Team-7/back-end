using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface ITouristEquipmentService
    {
        Result<PagedResult<TouristEquipmentDto>> GetPaged(int page, int pageSize);
        Result<TouristEquipmentDto> Create(TouristEquipmentDto touristEquipment);
        Result<TouristEquipmentDto> Update(TouristEquipmentDto touristEquipment);
        Result Delete(int Id);
    }
}
