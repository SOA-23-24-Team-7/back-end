using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{

    [Route("api/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService authenticationService)
        {
            _blogService = authenticationService;
        }


        [Authorize(Policy = "userPolicy")]
        [HttpPost("create")]
        public ActionResult<BlogResponseDto> Create([FromBody] BlogCreateDto blog)
        {
            blog.Date = DateTime.UtcNow;
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.GetAll(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<BlogResponseDto> Get(long id)
        {
            var result = _blogService.GetById(id);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BlogResponseDto> Update([FromBody] BlogUpdateDto blog, int id)
        {
            blog.Id = id;
            var result = _blogService.UpdateBlog(blog);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _blogService.Delete(id);
            return CreateResponse(result);
        }

        [Authorize(Policy = "userPolicy")]
        [HttpGet("upvote/{id:long}")]
        public ActionResult Upvote(long id)
        {
            if (_blogService.IsBlogClosed(id)) return CreateResponse(Result.Fail(FailureCode.InvalidArgument));

            var userId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _blogService.SetVote(id, userId, VoteType.UPVOTE);
            return CreateResponse(result);
        }

        [Authorize(Policy = "userPolicy")]
        [HttpGet("downvote/{id:long}")]
        public ActionResult Downvote(long id)
        {
            if (_blogService.IsBlogClosed(id)) return CreateResponse(Result.Fail(FailureCode.InvalidArgument));

            var userId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _blogService.SetVote(id, userId, VoteType.DOWNVOTE);
            return CreateResponse(result);
        }
    }

}

