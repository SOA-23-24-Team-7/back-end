using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

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
            _dbContext.KeyPoints.Add(keyPoint);
            _dbContext.SaveChanges();
            return keyPoint;
        }

        public List<KeyPoint> GetByTourId(long tourId)
        {
            var keyPoints = _dbContext.KeyPoints.Where(i => i.TourId == tourId);
            return keyPoints.ToList();
        }
    }
}
