using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IPersonService
    {
        Result<PersonResponseDto> Update<PersonUpdateDto>(PersonUpdateDto person);
        Result<PersonResponseDto> GetByUserId(long id);
        Result<PagedResult<PersonResponseDto>> GetPaged(int page, int pageSize);
    }
}
