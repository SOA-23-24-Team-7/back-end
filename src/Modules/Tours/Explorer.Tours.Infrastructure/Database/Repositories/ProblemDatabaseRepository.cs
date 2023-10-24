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
    public class ProblemDatabaseRepository:IProblemRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<Problem> _dbSet;
        public ProblemDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Problem>();
        }
        public PagedResult<Problem> GetByUserId(int page, int pageSize, int id)
        {
            var task = _dbSet.Where(x => x.TouristId == id).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
