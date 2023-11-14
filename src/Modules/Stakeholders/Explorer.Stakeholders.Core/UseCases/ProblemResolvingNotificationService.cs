using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemResolvingNotificationService : CrudService<ProblemResolvingNotificationResponseDto, ProblemResolvingNotification>, IProblemResolvingNotificationService
    {
        private readonly IProblemResolvingNotificationRepository _problemResolvingNotificationRepository;
        public ProblemResolvingNotificationService(ICrudRepository<ProblemResolvingNotification> repository, IMapper mapper, IProblemResolvingNotificationRepository problemResolvingNotificationRepository) : base(repository, mapper)
        {
            _problemResolvingNotificationRepository = problemResolvingNotificationRepository;
        }

        public Result<PagedResult<ProblemResolvingNotificationResponseDto>> GetByLoggedInUser(int page, int pageSize, long id)
        {
            var results = MapToDto<ProblemResolvingNotificationResponseDto>(_problemResolvingNotificationRepository.GetByLoggedInUser(page, pageSize, id));
            return results;
        }
        public Result<ProblemResolvingNotificationResponseDto> SetSeenStatus(long id)
        {
            try
            {
                ProblemResolvingNotification notification = CrudRepository.Get(id);
                if (!notification.HasSeen)
                {
                    notification.SetSeenStatus();
                    var result = CrudRepository.Update(notification);
                    return MapToDto<ProblemResolvingNotificationResponseDto>(result);
                }
                return Result.Ok();
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
