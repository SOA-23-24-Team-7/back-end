using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserService
    {
        Result<UserDto> DisableAccount(long userId);
    }
}
