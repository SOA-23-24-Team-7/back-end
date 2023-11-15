using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IPersonService
    {
        public Result<PersonResponseDto> UpdatePerson(PersonUpdateDto personData);
        Result<PersonResponseDto> GetByUserId(long id);
        Result<PersonResponseDto> Get(long id);
        Result<PagedResult<PersonResponseDto>> GetAll(int page, int pageSize);
        Result<PagedResult<PersonResponseDto>> GetPagedByAdmin(int page, int pageSize, long adminId);
    }
}
