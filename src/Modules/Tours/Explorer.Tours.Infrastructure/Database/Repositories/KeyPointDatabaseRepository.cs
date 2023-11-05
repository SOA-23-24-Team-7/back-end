using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class KeyPointDatabaseRepository : IKeyPointRepository
    {
        private readonly ToursContext _dbContext;

        public KeyPointDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
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
    }

}
