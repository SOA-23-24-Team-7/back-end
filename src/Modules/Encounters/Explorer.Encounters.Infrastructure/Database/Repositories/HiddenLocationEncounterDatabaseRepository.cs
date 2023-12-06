using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class HiddenLocationEncounterDatabaseRepository : IHiddenLocationEncounterRepository
    {

        private readonly EncountersContext _dbContext;
        private readonly DbSet<HiddenLocationEncounter> _dbSet;

        public HiddenLocationEncounterDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<HiddenLocationEncounter>();
        }

        public PagedResult<HiddenLocationEncounter> GetAll(int page, int pageSize)
        {
            var task = _dbSet.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public HiddenLocationEncounter GetHiddenLocationEncounterById(long id)
        {
            var entity = _dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }
    }
}
