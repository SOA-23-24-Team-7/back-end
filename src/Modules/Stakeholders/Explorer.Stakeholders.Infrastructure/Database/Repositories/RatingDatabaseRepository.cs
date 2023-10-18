using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class RatingDatabaseRepository : IRatingRepository
    {
        private readonly StakeholdersContext _dbContext;
        public RatingDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Rating? GetByUserId(int id)
        {
            var rating = _dbContext.Ratings.FirstOrDefault(r => r.UserId == id);
            return rating;
        }
    }
}
