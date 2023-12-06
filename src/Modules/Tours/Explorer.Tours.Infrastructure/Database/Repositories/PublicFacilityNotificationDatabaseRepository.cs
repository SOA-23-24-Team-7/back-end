using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PublicFacilityNotificationDatabaseRepository : IPublicFacilityNotificationRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<PublicFacilityNotification> _dbSet;
        public PublicFacilityNotificationDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<PublicFacilityNotification>();
        }
        public PagedResult<PublicFacilityNotification> GetByAuthorId(int page, int pageSize, long authorId)
        {
            var task = _dbSet.Where(x => x.AuthorId == authorId).GetPagedById(page, pageSize);
            //var task = _dbContext.Reviews.Include(r => r.Tour).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public int CountNotSeen(long userId)
        {
            return _dbSet.Count(x => !x.IsSeen && x.AuthorId == userId);
        }

        public void Update(PublicFacilityNotification notification)
        {
            try
            {
                _dbContext.Update(notification);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }
    }
}
