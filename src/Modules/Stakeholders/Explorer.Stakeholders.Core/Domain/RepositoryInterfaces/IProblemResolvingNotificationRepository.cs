using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.Problems;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IProblemResolvingNotificationRepository
    {
        PagedResult<ProblemResolvingNotification> GetByLoggedInUser(int page, int pageSize, long id);
        int CountNotSeen(long userId);
    }
}
