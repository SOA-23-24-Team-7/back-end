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

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPost]
        public ActionResult<CommentResponseDto> Create([FromBody] CommentCreateDto comment)
        {
            var authorId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            comment.AuthorId = authorId;
            comment.CreatedAt = DateTime.UtcNow;
            var result = _commentService.Create(comment);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPut("{commentId:long}")]
        public ActionResult<CommentResponseDto> Update([FromBody] CommentUpdateDto comment, long commentId)
        {
            comment.Id = commentId;
            var result = _commentService.UpdateComment(comment);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpDelete("{commentId:long}")]
        public ActionResult Delete(long commentId)
        {
            var senderId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var comment = _commentService.Get(commentId);
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
