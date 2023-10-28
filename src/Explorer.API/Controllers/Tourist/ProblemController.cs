using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/problem")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("all")]
        public ActionResult<PagedResult<ProblemResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ProblemResponseDto> Create([FromBody] ProblemCreateDto problem)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                problem.TouristId = long.Parse(identity.FindFirst("id").Value);
            }
            problem.DateTime = DateTime.Now;
            var result = _problemService.Create(problem);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProblemResponseDto> Update([FromBody] ProblemUpdateDto problem)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                if (long.Parse(identity.FindFirst("id").Value) != problem.TouristId)
                    return Forbid();
            }
            var result = _problemService.Update(problem);
            return CreateResponse(result);
        }

        [HttpGet("resolve/{problemId:long}")]
        public ActionResult<ProblemResponseDto> ResolveProblem(long problemId)
        {
            var result = _problemService.ResolveProblem(problemId);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _problemService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<ProblemResponseDto>> GetByUserId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByUserId(page, pageSize, int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value));
            return CreateResponse(result);
        }
    }
}
