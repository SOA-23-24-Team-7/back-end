using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ReviewDatabaseRepository : IReviewRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<Review> _dbSet;


        public ReviewDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Review>();
        }

        public PagedResult<Review> GetPagedByTourId(int page, int pageSize, long tourId)
        {
            var task = _dbSet.Where(x => x.TourId == tourId).GetPagedById(page, pageSize);
            //var task = _dbContext.Reviews.Include(r => r.Tour).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
        public bool ReviewExists(long touristId, long tourId)
        {
            var entity = _dbSet.Where(x => x.TouristId == touristId && x.TourId == tourId).FirstOrDefault();
            return entity != null;
        }
        public int GetTourReviewCounts(long tourId, int forLastNDays)
        {
            int count = _dbSet.Where(review => review.TourId == tourId && review.CommentDate.AddDays(forLastNDays).CompareTo(DateOnly.FromDateTime(DateTime.Now)) >= 0).Count();
            return count;
        }
        public double? GetTourReviewAverageRating(long tourId, int forLastNDays)
        {
            var allRatings = _dbSet.Where(review => review.TourId == tourId && review.CommentDate.AddDays(forLastNDays).CompareTo(DateOnly.FromDateTime(DateTime.Now)) >= 0).Select(review => review.Rating);
            if(!allRatings.Any())
            {
                return 0;
            }
            return allRatings.Average();
        }

        public int GetTourReviewCountsAllTime(long tourId)
        {
            int count = _dbSet.Where(review => review.TourId == tourId).Count();
            return count;
        }
        public double? GetTourReviewAverageRatingAllTime(long tourId)
        {
            var allRatings = _dbSet.Where(review => review.TourId == tourId).Select(review => review.Rating);
            if (!allRatings.Any())
            {
                return 0;
            }
            return allRatings.Average();
        }
    }
}
