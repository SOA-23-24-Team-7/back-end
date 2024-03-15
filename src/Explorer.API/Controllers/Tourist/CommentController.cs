using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/comment")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly IHttpClientService _httpClientService;

        public CommentController(ICommentService commentService, IBlogService blogService, IHttpClientService httpClientService)
        {
            _commentService = commentService;
            _blogService = blogService;
            _httpClientService = httpClientService;
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




        [HttpGet]
        public async Task<String> GetAll()
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8090, "comments");
            var response = await _httpClientService.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("comments/new")]
        public async Task<ActionResult<CommentResponseDto>> CreateComment([FromBody] CommentCreateDto comment)
        {
            comment.CreatedAt = DateTime.UtcNow;
           
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8090, "comments");

            try
            {
                var json = JsonConvert.SerializeObject(comment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClientService.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var goCommentResponse = JsonConvert.DeserializeObject<CommentResponseDto>(responseString);

                    return CreateResponse(Result.Ok(goCommentResponse));
                }
                else
                {
                    return StatusCode(500, "Error creating comment");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating comment");
            }
        }

        [HttpPut]
        [Route("comments/update")]
        public async Task<IActionResult> UpdateComment(long commentId, CommentUpdateDto comment)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8090, "comments/"+commentId);

            try
            {
                var json = JsonConvert.SerializeObject(comment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClientService.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var goCommentResponse = JsonConvert.DeserializeObject<CommentResponseDto>(responseString);

                    return CreateResponse(Result.Ok(goCommentResponse));
                }
                else
                {
                    return StatusCode(500, "Error updating comment");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error updating comment");
            }
        }


        [HttpDelete]
        [Route("comments/delete")]
        public async Task<IActionResult> DeleteComment(long commentId)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8090, "comments/" + commentId);
            try
            {
                var response = await _httpClientService.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    return Ok("Comment deleted successfully");
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return StatusCode(500, $"Error deleting comment: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting comment: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("comments/blogComments")]
        public async Task<String> GetAllBlogComments(long blogId)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8090, "blogComments/"+blogId);
            var response = await _httpClientService.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            else
            {
                return null;
            }
        }
    }
}
