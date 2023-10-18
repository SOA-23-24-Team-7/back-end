using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserService
    {
        Result<UserDto> DisableAccount(long userId);
        Result<PagedResult<UserDto>> GetPaged(int page, int pageSize);
    }
}
