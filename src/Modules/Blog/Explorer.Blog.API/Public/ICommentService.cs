using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface ICommentService
    {
        Result<CommentResponseDto> Create<CommentCreateDto>(CommentCreateDto commentData);
        Result<CommentResponseDto> UpdateComment(CommentUpdateDto commentData);
        Result Delete(long id);
        Result<CommentResponseDto> Get(long id);
        Result<PagedResult<CommentResponseDto>> GetPagedByBlogId(int page, int pageSize, long blogId);

    }
}
