using Microsoft.AspNetCore.Authorization;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public ActionResult<PagedResult<ProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
