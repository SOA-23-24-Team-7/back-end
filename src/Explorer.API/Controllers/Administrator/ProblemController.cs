using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/problem")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;
        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }
        [HttpGet]
        public ActionResult<PagedResult<ProblemResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
