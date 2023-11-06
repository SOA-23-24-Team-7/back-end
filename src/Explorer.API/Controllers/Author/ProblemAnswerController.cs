using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/problemAnswer")]
    public class ProblemAnswerController : BaseApiController
    {
        private readonly IProblemAnswerService _problemAnswerService;
        private readonly IProblemService _problemService;
        public ProblemAnswerController(IProblemAnswerService problemAnswerService, IProblemService problemService)
        {
            _problemAnswerService = problemAnswerService;
            _problemService = problemService;
        }

        [HttpPost]
        public ActionResult<ProblemAnswerResponseDto> Create([FromBody] ProblemAnswerCreateDto problemAnswer)
        {
            var loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var problem = _problemService.Get(problemAnswer.ProblemId).Value;
            if (loggedInUserId == problem.TourAuthorId)
            {
                if (!problem.IsAnswered & !problem.IsResolved)
                {
                    var result = _problemAnswerService.Create(problemAnswer);
                    _problemService.UpdateIsAnswered(problemAnswer.ProblemId, true);
                    _problemService.UpdateAnswerId(problemAnswer.ProblemId, result.Value.Id);
                    return CreateResponse(result);
                }
            }
            return Forbid();
        }
    }
}
