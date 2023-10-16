using Explorer.Blog.API.Dtos;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface ICommentService
    {
        Result<CommentDto> Create(CommentDto comment);
    }
}
