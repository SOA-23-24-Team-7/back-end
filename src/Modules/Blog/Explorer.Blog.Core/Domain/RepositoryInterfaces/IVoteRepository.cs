using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IVoteRepository
    {
        PagedResult<Vote> GetPagedByUserId(int page, int pageSize, long userId);
        Vote GetByUserIdAndBlogId(long userId, long blogId);

    }
}
