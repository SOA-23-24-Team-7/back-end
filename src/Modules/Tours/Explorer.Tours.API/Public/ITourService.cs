using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public;

public interface ITourService
{
    Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
    Result<TourDto> Create(TourDto tour);
    Result<TourDto> Update(TourDto tour);
    Result Delete(int id);
    Result<PagedResult<TourDto>> GetAuthorsPagedTours(long id,int page, int pageSize);
}
