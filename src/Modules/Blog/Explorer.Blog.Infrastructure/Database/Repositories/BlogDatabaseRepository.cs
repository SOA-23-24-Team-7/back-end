using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.Core.Domain;
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

        public PagedResult<Core.Domain.Blog> GetPagedByBlogId(int page, int pageSize, long blogId)
        {
            var task = _dbSet.Where(x => x.Id == blogId).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
