using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubRepository : CrudDatabaseRepository<Club, StakeholdersContext>, IClubRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedResult<Club> GetClubsPaged(int page, int pageSize)
        {
            var task = _dbContext.Clubs.Include(c => c.Owner).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
