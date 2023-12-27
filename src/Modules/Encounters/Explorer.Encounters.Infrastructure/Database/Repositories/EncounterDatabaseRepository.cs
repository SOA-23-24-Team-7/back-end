using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterDatabaseRepository : IEncounterRepository
    {
        private readonly EncountersContext _dbContext;
        private readonly DbSet<Encounter> _dbSet;


        public EncounterDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Encounter>();
        }

        public PagedResult<Encounter> GetActive(int page, int pageSize)
        {
            var task = _dbSet.Where(x => x.Status == EncounterStatus.Active).GetPaged(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Encounter> GetAll(int page, int pageSize)
        {
            var task = _dbSet.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Encounter> GetAllInRangeOf(double range, double longitude, double latitude, int page, int pageSize)
        {
            var allEncounters = _dbSet.ToList();
            var filteredEncounters = allEncounters.Where(x => x.IsInRangeOf(range, longitude, latitude)).ToList();
            var filteredIds = filteredEncounters.Select(e => e.Id).ToList();
            var task = _dbSet.Where(e => filteredIds.Contains(e.Id)).GetPaged(page, pageSize);
            task.Wait();

            return task.Result;
        }

        public PagedResult<Encounter> GetAllDoneByUser(int currentUserId, int page, int pageSize)
        {
            var allEncounters = _dbSet.ToList();
            var doneEncounters = allEncounters.Where(encounter =>
                encounter.Instances.Any(instance => instance.UserId == currentUserId)).ToList();

            var filteredIds = doneEncounters.Select(e => e.Id).ToList();
            var task = _dbSet.Where(e => filteredIds.Contains(e.Id)).GetPaged(page, pageSize);
            task.Wait();

            return task.Result;
        }




        public Encounter GetById(long id)
        {
            var entity = _dbSet.First(x => x.Id == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }
        public EncounterInstance GetInstance(long userId, long encounterId)
        {
            var entity = _dbSet.First(x => x.Id == encounterId);
            var instance = entity.Instances.FirstOrDefault(x => x.UserId == userId);
            return instance;
        }
    }
}
