using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : CrudService<UserResponseDto, User>, IUserService, IInternalUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IFollowerRepository _followerRepository;

        public UserService(ICrudRepository<User> repository, IFollowerRepository followerRepository, IUserRepository userRepository, IMapper mapper) : base(
            repository, mapper)
        {
            _userRepository = userRepository;
            _followerRepository = followerRepository;
        }

        public Result<UserResponseDto> DisableAccount(long userId)
        {
            try
            {
                User user = CrudRepository.Get(userId);
                user.IsActive = false;

                var result = CrudRepository.Update(user);
                return MapToDto<UserResponseDto>(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<string> GetNameById(long id)
        {
            return _userRepository.GetNameById(id);
        }

        public string GetProfilePicture(long adminId)
        {
            return _userRepository.GetProfilePicture(adminId);
        }

        public Result<PagedResult<UserResponseDto>> GetPagedByAdmin(int page, int pageSize, long adminId)
        {
            return MapToDto<UserResponseDto>(_userRepository.GetPagedByAdmin(page, pageSize, adminId));
        }

        public Result<PagedResult<UserResponseDto>> SearchUsers(int page, int pageSize, string searchUserName, long id)
        {
            return MapToDto<UserResponseDto>(_userRepository.SearchUsers(page, pageSize, searchUserName, id));
        }

        public Result<UserResponseDto> UpdateProfilePicture(long userId, string profilePicture)
        {
            try
            {
                var user = CrudRepository.Get(userId);
                user.UpdateProfilePicture(profilePicture);
                var result = CrudRepository.Update(user);
                return MapToDto<UserResponseDto>(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
