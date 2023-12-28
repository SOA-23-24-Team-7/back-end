using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogDatabaseRepository : IBlogRepository
    {
        private readonly BlogContext _dbContext;
        private readonly DbSet<Core.Domain.Blog> _dbSet;


        public BlogDatabaseRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Core.Domain.Blog>();
        }

        public PagedResult<Core.Domain.Blog> GetAll(int page, int pageSize)
        {
            var task = _dbSet.Include(x => x.Comments)
                .Where(blog => blog.ClubId == null)
                .GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public PagedResult<Core.Domain.Blog> GetAllFromClub(int page, int pageSize, long ClubId)
        {
            var task = _dbSet.Include(x => x.Comments)
                .Where(blog => blog.ClubId == ClubId)
                .GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Core.Domain.Blog GetById(long id)
        {
            var entity = _dbSet.Include(x => x.Comments).First(x => x.Id == id);
            if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }
    }
}
