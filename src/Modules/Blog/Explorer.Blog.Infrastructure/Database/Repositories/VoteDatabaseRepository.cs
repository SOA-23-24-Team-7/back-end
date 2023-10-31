using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class VoteDatabaseRepository : IVoteRepository
    {
        private readonly BlogContext _dbContext;
        private readonly DbSet<Vote> _dbSet;

        public VoteDatabaseRepository(BlogContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Vote>();
        }

        public PagedResult<Vote> GetPagedByUserId(int page, int pageSize, long userId)
        {
            var task = _dbSet.Where(x => x.UserId == userId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Vote GetByUserIdAndBlogId(long userId, long blogId)
        {
            var entity = _dbSet.Where(x => x.UserId == userId && x.BlogId == blogId).FirstOrDefault();
            if (entity == null) throw new KeyNotFoundException("Not found: " + userId + ", " + blogId);
            return entity;
        }
    }
}
