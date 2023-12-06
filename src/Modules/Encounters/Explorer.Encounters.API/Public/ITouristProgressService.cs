using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;

namespace Explorer.Encounters.API.Public
{
    public interface ITouristProgressService
    {
        Result<TouristProgressResponseDto> Create<TouristProgressCreateDto>(TouristProgressCreateDto encounter);
        Result<PagedResult<TouristProgressResponseDto>> GetPaged(int page, int pageSize);
        Result<TouristProgressResponseDto> Get(long id);
        Result Delete(long id);
        Result<TouristProgressResponseDto> GetByUserId(long userId);
    }
}
