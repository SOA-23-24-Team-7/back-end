using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ProblemDatabaseRepository : IProblemRepository
    {
        private readonly StakeholdersContext _dbContext;
        private readonly DbSet<Problem> _dbSet;
        public ProblemDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Problem>();
        }

        public PagedResult<Problem> GetByUserId(int page, int pageSize, long id)
        {
            var task = _dbSet.Include(x => x.Tourist).
                Where(x => x.TouristId == id).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Problem> GetByAuthor(int page, int pageSize, List<long> tourIds)
        {
            var task = _dbSet.Include(x => x.Tourist).
                Where(x => tourIds.Contains(x.TourId)).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Problem> GetAll(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.Tourist).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Problem Get(long id)
        {
            var task = _dbContext.Problem.Include(x => x.Tourist).FirstOrDefault(problem => problem.Id == id);
            return task;
        }

        public Problem GetByAnswerId(long id)
        {
            //var task = _dbContext.Problem.FirstOrDefault(problem => problem.AnswerId == id);
            return null;
        }

        public void DeleteByTour(long tourId)
        {
            _dbContext.Problem.Where(x => x.TourId == tourId).ExecuteDelete();
            _dbContext.SaveChanges();
        }
    }
}
