using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "nonAdministratorPolicy")]
    [Route("api/rating/rating")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public ActionResult<RatingResponseDto> Create([FromBody] RatingCreateDto rating)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                rating.UserId = long.Parse(identity.FindFirst("id").Value);
            }
            rating.DateTime = DateTime.UtcNow.AddHours(1);
            var result = _ratingService.Create(rating);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<RatingResponseDto> Update([FromBody] RatingUpdateDto rating)
        {
            var result = _ratingService.Update(rating);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _ratingService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<RatingResponseDto> GetByUser(long id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                if(id != long.Parse(identity.FindFirst("id").Value))
                {
                    throw new ArgumentException("Not active UserId");
                }
            }
            var result = _ratingService.GetByUser(id);
            return CreateResponse(result);
        }
    }
}
