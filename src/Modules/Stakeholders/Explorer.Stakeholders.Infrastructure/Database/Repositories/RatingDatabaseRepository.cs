using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class RatingDatabaseRepository : IRatingRepository
    {
        private readonly StakeholdersContext _dbContext;
        public RatingDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Rating? GetByUserId(long id)
        {
            var rating = _dbContext.Ratings.FirstOrDefault(r => r.UserId == id);
            return rating;
        }
        public PagedResult<Rating> GetRatingsPaged(int page, int pageSize)
        {
            var task = _dbContext.Ratings.Include(r => r.User).GetPagedById(page, pageSize);

            task.Wait();
            return task.Result;
        }
    }
}
