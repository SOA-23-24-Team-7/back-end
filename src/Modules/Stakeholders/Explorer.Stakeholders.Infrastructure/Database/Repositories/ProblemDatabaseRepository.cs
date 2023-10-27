using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProblemDatabaseRepository:IProblemRepository
    {
        private readonly StakeholdersContext _dbContext;
        private readonly DbSet<Problem> _dbSet;
        public ProblemDatabaseRepository(StakeholdersContext dbContext)
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
