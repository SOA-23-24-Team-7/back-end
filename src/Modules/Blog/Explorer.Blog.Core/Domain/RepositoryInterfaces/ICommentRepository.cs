using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        PagedResult<Comment> GetPagedByBlogId(int page, int pageSize, long blogId);
    }
}
