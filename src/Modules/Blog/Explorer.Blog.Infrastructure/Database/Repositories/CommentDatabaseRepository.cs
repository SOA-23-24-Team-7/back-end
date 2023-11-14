using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class CommentDatabaseRepository : ICommentRepository
    {
        private readonly BlogContext _dbContext;
        private readonly DbSet<Comment> _dbSet;

        public CommentDatabaseRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Comment>();
        }

        public PagedResult<Comment> GetPagedByBlogId(int page, int pageSize, long blogId)
        {
            var task = _dbSet.Where(x => x.BlogId == blogId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
