using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface IBlogService
    {
        Result<BlogResponseDto> Create<BlogCreateDto>(BlogCreateDto blog);
        Result<PagedResult<BlogResponseDto>> GetPaged(int page, int pageSize);
        Result<BlogResponseDto> Get(long id);
        Result<BlogResponseDto> UpdateBlog(BlogUpdateDto blog);
        public Result Delete(long id);
    }
}
