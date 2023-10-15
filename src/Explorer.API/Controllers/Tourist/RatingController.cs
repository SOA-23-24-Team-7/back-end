using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/rating/rating")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public ActionResult<RatingDto> Create([FromBody] RatingDto rating)
        {
            //var userid = User.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
            
            var result = _ratingService.Create(rating);
            return CreateResponse(result);
        }
    }
}
