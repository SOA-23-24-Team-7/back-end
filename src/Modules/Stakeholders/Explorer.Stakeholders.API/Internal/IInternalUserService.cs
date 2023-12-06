using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalUserService
    {
        Result<string> GetNameById(long id);
        Result<UserResponseDto> Get(long id);
        string GetProfilePicture(long adminId);
    }
}
