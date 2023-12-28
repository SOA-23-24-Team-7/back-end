using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/comment")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;

        public CommentController(ICommentService commentService, IBlogService blogService)
        {
            _commentService = commentService;
            _blogService = blogService;
        }

        [Authorize(Policy = "userPolicy")]
        [HttpPost]
        public ActionResult<CommentResponseDto> Create([FromBody] CommentCreateDto comment)
        {
            if (_blogService.IsBlogClosed(comment.BlogId)) return CreateResponse(Result.Fail(FailureCode.InvalidArgument));

            var authorId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            comment.AuthorId = authorId;
            comment.CreatedAt = DateTime.UtcNow;
            var result = _commentService.Create(comment);
            return CreateResponse(result);
        }

        [Authorize(Policy = "userPolicy")]
        [HttpPut("{commentId:long}")]
        public ActionResult<CommentResponseDto> Update([FromBody] CommentUpdateDto commentData, long commentId)
        {
            var senderId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var comment = _commentService.Get(commentId);

            if (_blogService.IsBlogClosed(comment.Value.BlogId)) return CreateResponse(Result.Fail(FailureCode.InvalidArgument));


            if (senderId != comment.Value.AuthorId)
            {
                return CreateResponse(Result.Fail(FailureCode.Forbidden));
            }
            commentData.Id = commentId;
            var result = _commentService.UpdateComment(commentData);
            return CreateResponse(result);
        }

        [Authorize(Policy = "userPolicy")]
        [HttpDelete("{commentId:long}")]
        public ActionResult Delete(long commentId)
        {
            var senderId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var comment = _commentService.Get(commentId);

            if (_blogService.IsBlogClosed(comment.Value.BlogId)) return CreateResponse(Result.Fail(FailureCode.InvalidArgument));


            if (senderId != comment.Value.AuthorId)
            {
                return CreateResponse(Result.Fail(FailureCode.Forbidden));
            }
            var result = _commentService.Delete(commentId);
            return CreateResponse(result);

        }

        [HttpGet("{blogId:long}")]
        public ActionResult<PagedResult<CommentResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize, long blogId)
        {
            var result = _commentService.GetPagedByBlogId(page, pageSize, blogId);
            return CreateResponse(result);
        }
    }
}
