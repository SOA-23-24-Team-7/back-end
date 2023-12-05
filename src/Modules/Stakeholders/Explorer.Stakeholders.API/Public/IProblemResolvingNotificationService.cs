using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProblemResolvingNotificationService
    {
        Result<ProblemResolvingNotificationResponseDto> Create<ProblemResolvingNotificationCreateDto>(ProblemResolvingNotificationCreateDto notification);
        public Result<PagedResult<ProblemResolvingNotificationResponseDto>> GetByLoggedInUser(int page, int pageSize, long id);
        Result<ProblemResolvingNotificationResponseDto> SetSeenStatus(long id);
        Result<int> CountUnseenNotifications(long userId);
    }
}
