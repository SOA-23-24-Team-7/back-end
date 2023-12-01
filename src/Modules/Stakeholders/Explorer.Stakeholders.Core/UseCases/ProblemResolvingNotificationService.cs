using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemResolvingNotificationService : CrudService<ProblemResolvingNotificationResponseDto, ProblemResolvingNotification>, IProblemResolvingNotificationService
    {
        private readonly IProblemResolvingNotificationRepository _problemResolvingNotificationRepository;
        private readonly IInternalNotificationService _internalNotificationService;
        public ProblemResolvingNotificationService(ICrudRepository<ProblemResolvingNotification> repository, IMapper mapper, IProblemResolvingNotificationRepository problemResolvingNotificationRepository, IInternalNotificationService internalNotificationService) : base(repository, mapper)
        {
            _problemResolvingNotificationRepository = problemResolvingNotificationRepository;
            _internalNotificationService = internalNotificationService;
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

        public Result<int> CountUnseenNotifications(long userId)
        {
            try
            {
                int count = 0;
                count += _problemResolvingNotificationRepository.CountNotSeen(userId);
                count += _internalNotificationService.CountNotSeen(userId);

                return count;
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
