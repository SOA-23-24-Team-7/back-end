using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.Encounter;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class MiscEncounterDatabaseRepository : IMiscEncounterRepository
    {

        private readonly EncountersContext _dbContext;
        private readonly DbSet<MiscEncounter> _dbSet;

        public MiscEncounterDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<MiscEncounter>();
        }

        public PagedResult<MiscEncounter> GetAll(int page, int pageSize)
        {
            var task = _dbSet.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public MiscEncounter GetById(long id)
        {
            var entity = _dbSet.First(x => x.Id == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }
    }


}

