using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "nonAdministratorPolicy")]
    [Route("api/problemComment")]
    public class ProblemCommentController : BaseApiController
    {
        private readonly IProblemCommentService _problemCommentService;
        private readonly IProblemService _problemService;

        public ProblemCommentController(IProblemCommentService problemCommentService, IProblemService problemService)
        {
            _problemCommentService = problemCommentService;
            _problemService = problemService;
        }

        [HttpPost]
        public ActionResult<ProblemCommentResponseDto> Create([FromBody] ProblemCommentCreateDto problemComment)
        {
            var loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var touristId = 1;
            var authorId = 1;
            //ulogovani korisnik mora biti ili kreator ture ili kreator problema, ostali ne mogu da komentarisu
            if (loggedInUserId == touristId || loggedInUserId == authorId)
            {
                var result = _problemCommentService.Create(problemComment);
                return CreateResponse(result);
            }
            return Forbid();
        }

        [HttpGet("{id:long}")]
        public ActionResult<PagedResult<ProblemResponseDto>> GetPagedByProblemAnswerId([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            var result = _problemCommentService.GetPagedByProblemAnswerId(page, pageSize, id);
            return CreateResponse(result);
        }

    }
}
