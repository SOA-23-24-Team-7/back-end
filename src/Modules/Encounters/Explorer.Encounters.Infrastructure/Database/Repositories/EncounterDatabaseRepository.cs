using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
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
        private readonly DbSet<Encounter.Core.Domain.Encounter> _dbSet;


        public EncounterDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Encounter.Core.Domain.Encounter>();
        }

        public PagedResult<Encounter.Core.Domain.Encounter> GetActive(int page, int pageSize)
        {
            var task = _dbSet.Where(x=>x.Status==Encounter.Core.Domain.EncounterStatus.Active).GetPaged(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
