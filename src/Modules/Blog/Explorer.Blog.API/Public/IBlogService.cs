using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface IBlogService
    {
        Result<BlogResponseDto> Create<BlogCreateDto>(BlogCreateDto blog);
        Result<BlogResponseDto> Update<BlogUpdateDto>(BlogUpdateDto blog);
        Result<PagedResult<BlogResponseDto>> GetAll(int page, int pageSize);
        Result<BlogResponseDto> GetById(long id);
        Result SetVote(long blogId, long userId, VoteType voteType);
        bool IsBlogClosed(long blogId);
        Result<PagedResult<BlogResponseDto>> GetPaged(int page, int pageSize);
        Result<BlogResponseDto> Get(long id);
        Result<BlogResponseDto> UpdateBlog(BlogUpdateDto blog);
        public Result Delete(long id);
        Result<PagedResult<BlogResponseDto>> GetAllFromBlogs(int page, int pageSize, long clubId);
    }
}
