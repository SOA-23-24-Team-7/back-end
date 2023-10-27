using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ProblemDatabaseRepository : IProblemRepository
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

        public PagedResult<Problem> GetByAuthor(int page, int pageSize, List<TourResponseDto> authorsTours)
        {
            var tourIds = authorsTours.Select(x => x.Id);
            var task = _dbSet.Where(x => tourIds.Contains(x.TourId)).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
