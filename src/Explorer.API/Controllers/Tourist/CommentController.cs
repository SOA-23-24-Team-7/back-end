using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using FluentResults;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Google.Protobuf.WellKnownTypes;


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
        public async Task<List<CommentResponse>> GetAll()
        {
            /*string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "comments");
            var response = await _httpClientService.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            else
            {
                return null;
            }*/
            using var channel = GrpcChannel.ForAddress("http://blog-service:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.GetAllComments(new Empty { });
            List<CommentResponse> comments = new List<CommentResponse>();
            foreach (var comment in reply.Comments)
            {
                comments.Add(comment);
            }
            return comments;
        }

        [HttpPost]
        [Route("comments/new")]
        public async Task<CommentResponse> CreateComment([FromBody] CommentCreateDto comment)
        {
            /*var authorId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            comment.AuthorId = authorId;
            comment.CreatedAt = DateTime.UtcNow;
           
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "comments");

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
            }*/
            using var channel = GrpcChannel.ForAddress("http://blog-service:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.CreateComment(new CommentCreationRequest { AuthorId = comment.AuthorId, BlogId = comment.BlogId, CreatedAt = Timestamp.FromDateTime(comment.CreatedAt), Text = comment.Text });
            return reply;
        }

        [HttpPut("comments/update/{id:long}")]
        public async Task<StringMessage> UpdateComment(long id, CommentUpdateDto comment)
        {
            /* string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "comments/"+id);

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
             }*/
            using var channel = GrpcChannel.ForAddress("http://blog-service:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.UpdateComment(new CommentUpdateRequest { Id = id , Text = comment.Text});
            return reply;
        }

        [HttpDelete("comments/delete/{id:long}")]
        public async Task<StringMessage> DeleteComment(long id)
        {
            /*string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, "comments/" + id);
            try
            {
                var response = await _httpClientService.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    return CreateResponse(Result.Ok());
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
            }*/

            using var channel = GrpcChannel.ForAddress("http://blog-service:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.DeleteComment(new CommentIdRequest { Id = id });
            return reply;
        }

        [HttpGet("comments/blogComments/{id:long}")]
        public async Task<List<CommentResponse>> GetAllBlogComments(long id)
        {
            /*string uri = _httpClientService.BuildUri(Protocol.HTTP, "blog-service", 8088, $"blogComments/{id}");
            var response = await _httpClientService.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            else
            {
                return null;
            }*/
            using var channel = GrpcChannel.ForAddress("http://blog-service:8088");
            var client = new BlogMicroservice.BlogMicroserviceClient(channel);
            var reply = client.GetAllBlogComments(new BlogIdRequest { Id = id });
            List<CommentResponse> comments = new List<CommentResponse>();
            foreach (var comment in reply.Comments)
            {
                comments.Add(comment);
            }
            return comments;
        }
    }
}
