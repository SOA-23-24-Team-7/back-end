using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PreferenceDatabaseRepository : IPreferenceRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<Preference> _dbSet;

        public PreferenceDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Preference Create(Preference preference)
        {
            _dbContext.Preferences.Add(preference);
            _dbContext.SaveChanges();
            return preference;
        }

        public Preference Update(Preference preference)
        {
            _dbContext.Preferences.Update(preference);
            _dbContext.SaveChanges();
            return preference;
        }

        public Preference GetByUserId(int userId)
        {
            var preference = _dbContext.Preferences;
            var found = preference.FirstOrDefault(tp => tp.UserId == userId);
            return found;
        }

        public Preference Get(long id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }

        public void Delete(long preferenceId) 
        {
            var entity = Get(preferenceId);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
