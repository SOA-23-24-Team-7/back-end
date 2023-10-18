using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface IBlogService
    {
        Result<BlogDto> Create(BlogDto blog);
        Result<PagedResult<BlogDto>> GetPaged(int page, int pageSize);
    }
}
