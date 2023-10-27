using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Route("api/author/problem")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet]
        public ActionResult<PagedResult<Problem>> GetByAuthor([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByAuthor(page, pageSize, long.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value));
            return CreateResponse(result);
        }
    }
}
