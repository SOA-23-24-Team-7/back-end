using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class TouristProgressDatabaseRepository : ITouristProgressRepository
    {
        private readonly EncountersContext _dbContext;
        private readonly DbSet<TouristProgress> _dbSet;

        public TouristProgressDatabaseRepository(EncountersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TouristProgress>();
        }

        public TouristProgress GetByUserId(long id)
        {
            var entity = _dbSet.First(x => x.UserId == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }
    }
}
