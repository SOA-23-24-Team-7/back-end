using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class TourPreferenceDatabaseRepository : ITourPreferenceRepository
    {
        private readonly StakeholdersContext _dbContext;
        private readonly DbSet<TourPreference> _dbSet;

        public TourPreferenceDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TourPreference Create(TourPreference tourPreference)
        {
            _dbContext.TourPreferences.Add(tourPreference);
            _dbContext.SaveChanges();
            return tourPreference;
        }

        public TourPreference GetByUserId(int userId)
        {
            var preference = _dbContext.TourPreferences;
            var found = preference.FirstOrDefault(tp => tp.UserId == userId);
            if (found == null) throw new KeyNotFoundException("Not found!");
            return found;
        }

        public TourPreference Get(long id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }

        public void Delete(int preferenceId) 
        {
            var entity = Get(preferenceId);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
