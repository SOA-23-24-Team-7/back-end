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
        public ProblemAnswerController(IProblemAnswerService problemAnswerService)
        {
            _problemAnswerService = problemAnswerService;
        }

        [HttpPost]
        public ActionResult<ProblemAnswerResponseDto> Create([FromBody] ProblemAnswerCreateDto problemAnswer)
        {
            var result = _problemAnswerService.Create(problemAnswer);
            return CreateResponse(result);
        }

    }
}
