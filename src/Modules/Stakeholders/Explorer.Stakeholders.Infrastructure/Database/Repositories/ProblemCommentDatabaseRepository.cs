using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    //public class ProblemCommentDatabaseRepository : IProblemCommentRepository
    //{
    //    private readonly StakeholdersContext _dbContext;
    //    private readonly DbSet<ProblemComment> _dbSet;

    //    public ProblemCommentDatabaseRepository(StakeholdersContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //        _dbSet = _dbContext.Set<ProblemComment>();
    //    }

    //    public PagedResult<ProblemComment> GetPagedByProblemAnswerId(int page, int pageSize, long problemAnswerId)
    //    {
    //        var task = _dbSet.Include(x => x.Commenter).Where(x => x.ProblemAnswerId == problemAnswerId).OrderBy(x => x.Id).GetPagedById(page, pageSize);
    //        task.Wait();
    //        return task.Result;
    //    }
    //}
}
