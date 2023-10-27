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

        public ProblemCommentController(IProblemCommentService problemCommentService)
        {
            _problemCommentService = problemCommentService;
        }

        [HttpPost]
        public ActionResult<ProblemCommentResponseDto> Create([FromBody] ProblemCommentCreateDto problemComment)
        {
            var result = _problemCommentService.Create(problemComment);
            return CreateResponse(result);
        }

    }
}
