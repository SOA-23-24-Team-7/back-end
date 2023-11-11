using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface IFacilityService
{
    Result<PagedResult<FacilityResponseDto>> GetPaged(int page, int pageSize);
    Result<PagedResult<FacilityResponseDto>> GetPagedByAuthorId(int page, int pageSize, int authorId);
    Result<FacilityResponseDto> Create<FacilityCreateDto>(FacilityCreateDto facility);
    Result<FacilityResponseDto> Update<FacilityUpdateDto>(FacilityUpdateDto facility);
    Result Delete(long id);
    Result<PagedResult<FacilityResponseDto>> GetPublic();
}