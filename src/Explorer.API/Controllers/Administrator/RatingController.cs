using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/rating/ratings")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet]
        public ActionResult<PagedResult<RatingDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _ratingService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}
