using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
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

        [Authorize(Policy = "nonAdministratorPolicy")]
        [HttpPost]
        public ActionResult<ProblemCommentResponseDto> Create([FromBody] ProblemCommentCreateDto problemComment)
        {
            var loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var problem = _problemService.GetByAnswerId(problemComment.ProblemAnswerId).Value;
            if ((loggedInUserId == problem.TouristId || loggedInUserId == problem.TourAuthorId) && !problem.IsResolved && problem.IsAnswered)
            {
                var result = _problemCommentService.Create(problemComment);
                return CreateResponse(result);
            }
            return Forbid();
        }

        [Authorize(Policy = "userPolicy")]
        [HttpGet("{id:long}")]
        public ActionResult<PagedResult<ProblemResponseDto>> GetPagedByProblemAnswerId([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            var result = _problemCommentService.GetPagedByProblemAnswerId(page, pageSize, id);
            return CreateResponse(result);
        }

    }
}
