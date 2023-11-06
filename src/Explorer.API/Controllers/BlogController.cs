using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
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
        public ActionResult<BlogResponseDto> Create([FromBody] BlogResponseDto blog)
        {
            blog.Date = DateTime.UtcNow;
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.GetPaged(page, pageSize);
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

        [HttpGet("{blogId:long}")]
        public ActionResult<PagedResult<BlogResponseDto>> Get(long blogId)
        {
            var result = _blogService.Get(blogId);
            return CreateResponse(result);
        }
    }

}

