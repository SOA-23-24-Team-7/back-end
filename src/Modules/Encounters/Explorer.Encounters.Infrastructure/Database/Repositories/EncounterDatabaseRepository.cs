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
            var task = _dbSet.Where(x=>x.Status==EncounterStatus.Active).GetPaged(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
