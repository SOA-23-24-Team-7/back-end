using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface IFacilityService
{
    Result<PagedResult<FacilityDto>> GetPaged(int page, int pageSize);
    Result<PagedResult<FacilityDto>> GetPagedByAuthorId(int page, int pageSize, int authorId);
    Result<FacilityDto> Create(FacilityDto facility);
    Result<FacilityDto> Update(FacilityDto facility);
    Result Delete(int id);
}