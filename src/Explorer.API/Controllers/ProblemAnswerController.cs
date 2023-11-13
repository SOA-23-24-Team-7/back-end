using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/problem-answer")]
    public class ProblemAnswerController : BaseApiController
    {
        //private readonly IProblemAnswerService _problemAnswerService;

        //public ProblemAnswerController(IProblemAnswerService problemAnswerService)
        //{
        //    _problemAnswerService = problemAnswerService;
        //}

        //[HttpGet("problem/{problemId:long}")]
        //public ActionResult<ProblemAnswerResponseDto> Get(long problemId)
        //{
        //    var result = _problemAnswerService.GetByProblem(problemId);
        //    return CreateResponse(result);
        //}
    }
}
