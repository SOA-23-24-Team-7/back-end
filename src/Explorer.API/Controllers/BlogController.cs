using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
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
            var result = _blogService.Create(blog);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.GetAll(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<BlogResponseDto> Get(int id)
        {
            var result = _blogService.GetById(id);
            return CreateResponse(result);
        }

    }
}
