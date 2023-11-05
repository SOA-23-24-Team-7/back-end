using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProblemAnswerDatabaseRepository : IProblemAnswerRepository
    {
        private readonly StakeholdersContext _dbContext;
        private readonly DbSet<ProblemAnswer> _dbSet;
        public ProblemAnswerDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProblemAnswer>();
        }

        public bool DoesAnswerExistsForProblem(long problemId)
        {
            bool task = _dbSet.Any(obj => obj.ProblemId == problemId);
            return task;
        }
    }
}
