using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : CrudService<UserDto, User>, IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(ICrudRepository<User> repository, IUserRepository userRepository, IMapper mapper) : base(
            repository, mapper)
        {
            _userRepository = userRepository;
        }

        public Result<UserDto> DisableAccount(long userId)
        {
            try
            {
                User user = CrudRepository.Get(userId);
                user.IsActive = false;

                var result = CrudRepository.Update(user);
                return MapToDto(result);
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

        public Result<PagedResult<UserDto>> GetPagedByAdmin(int page, int pageSize, long adminId)
        {
            return MapToDto(_userRepository.GetPagedByAdmin(page, pageSize, adminId));
        }
    }
}
