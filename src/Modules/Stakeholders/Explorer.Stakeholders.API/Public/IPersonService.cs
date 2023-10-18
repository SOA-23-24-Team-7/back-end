using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IPersonService
    {
        Result<PersonDto> Update(PersonDto personDto);
        Result<PersonDto> GetByUserId(long id);
        Result<PagedResult<PersonDto>> GetPaged(int page, int pageSize);
    }
}
