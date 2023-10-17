using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
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

        [HttpPut("{id:int}")]
        public ActionResult<RatingDto> Update([FromBody] RatingDto rating)
        {
            var result = _ratingService.Update(rating);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _ratingService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<RatingDto> GetByUser(int id)
        {
            var result = _ratingService.GetByUser(id);
            return CreateResponse(result);
        }
    }
}
