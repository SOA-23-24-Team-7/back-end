using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface ITourService
{
    Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
    Result<TourDto> Create<TourCreateDto>(TourCreateDto tour);
    Result<TourDto> Update<TourUpdateDto>(TourUpdateDto tour);
    Result Delete(long id);
    Result<PagedResult<TourDto>> GetAuthorsPagedTours(long id,int page, int pageSize);
}
