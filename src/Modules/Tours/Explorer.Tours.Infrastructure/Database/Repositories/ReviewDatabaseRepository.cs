using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

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

        public PagedResult<Review> GetPagedByTourId(int page, int pageSize, int tourId)
        {
            var task = _dbSet.Where(x => x.TourId == tourId).GetPagedById(page, pageSize);  
            task.Wait();
            return task.Result;
        }
    }
}
