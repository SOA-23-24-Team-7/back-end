using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProblemResolvingNotificationDatabaseRepository : IProblemResolvingNotificationRepository
    {
        private readonly StakeholdersContext _dbContext;
        private readonly DbSet<ProblemResolvingNotification> _dbSet;
        public ProblemResolvingNotificationDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProblemResolvingNotification>();
        }

        public PagedResult<ProblemResolvingNotification> GetByLoggedInUser(int page, int pageSize, long id)
        {
            var task = _dbSet.Include(x => x.Sender).
                Where(x => x.ReceiverId == id).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public int CountNotSeen(long userId)
        {
            return _dbSet.Count(x => !x.HasSeen && x.ReceiverId == userId);
        }
    }
}
