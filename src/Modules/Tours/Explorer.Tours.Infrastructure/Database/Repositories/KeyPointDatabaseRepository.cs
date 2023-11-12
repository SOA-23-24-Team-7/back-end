using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class KeyPointDatabaseRepository : IKeyPointRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<KeyPoint> _dbSet;

        public KeyPointDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<KeyPoint>();
        }

        public KeyPoint Create(KeyPoint keyPoint)
        {
            var tour = _dbContext.Tours
                             .Include(t => t.KeyPoints)
                             .Single(t => t.Id == keyPoint.TourId);

            tour.KeyPoints.Add(keyPoint);

            _dbContext.KeyPoints.Add(keyPoint);
            _dbContext.SaveChanges();
            return keyPoint;
        }

        public KeyPoint Get(long id)
        {
            var keyPoint = _dbContext.KeyPoints.Find(id);
            if (keyPoint == null) throw new KeyNotFoundException("Not found: " + id);
            return keyPoint;
        }

        public List<KeyPoint> GetByTourId(long tourId)
        {
            var keyPoints = _dbContext.KeyPoints.Where(i => i.TourId == tourId).OrderBy(i => i.Order);
            return keyPoints.ToList();
        }

        public List<KeyPoint> GetByTourIdWithoutSercrets(long tourId)
        {
            var keyPoints = _dbContext.KeyPoints.Where(i => i.TourId == tourId).OrderBy(i => i.Order);
            List<KeyPoint> keyPointsWithoutSecret = keyPoints.ToList();
            keyPointsWithoutSecret.ForEach(kp => kp.HideSecret());
            return keyPointsWithoutSecret;
        }

        public KeyPoint Update(KeyPoint keyPoint)
        {
            try
            {
                _dbContext.Update(keyPoint);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return keyPoint;
        }

        public void Delete(long id)
        {
            var keyPoint = Get(id);

            var tour = _dbContext.Tours
                                 .Include(t => t.KeyPoints)
                                 .Single(t => t.Id == keyPoint.TourId);

            tour.KeyPoints.Remove(keyPoint);

            _dbContext.KeyPoints.Remove(keyPoint);
            _dbContext.SaveChanges();
        }

        public KeyPoint GetFirstByTourId(long tourId)
        {
            var firstKeyPoint = _dbContext.KeyPoints.Where(i => i.TourId == tourId).OrderBy(i => i.Order).FirstOrDefault();
            return firstKeyPoint;
        }

        public PagedResult<KeyPoint> GetPaged(int page, int pageSize)
        {
            var task = _dbSet.GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
