using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserService
    {
        Result<UserResponseDto> Get(long userId);
        Result<UserResponseDto> DisableAccount(long userId);
        Result<PagedResult<UserResponseDto>> GetPagedByAdmin(int page, int pageSize, long adminId);
        Result<UserResponseDto> UpdateProfilePicture(long userId, string profilePicture);
        Result<PagedResult<UserResponseDto>> SearchUsers(int page, int pageSize, string searchUserName, long id);
    }
}
