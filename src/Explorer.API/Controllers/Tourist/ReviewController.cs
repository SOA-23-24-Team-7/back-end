using System.Security.Claims;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers.Tourist
{
    
    [Route("api/review")]
    public class ReviewController : BaseApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        //[Authorize(Policy = "nonAdministratorPolicy")]
        [HttpGet("{tourId:int}")]
        public ActionResult<PagedResult<ReviewResponseDto>> GetAllByTourId([FromQuery] int page, [FromQuery] int pageSize, long tourId)
        {
            var result = _reviewService.GetPagedByTourId(page, pageSize, tourId);
            return CreateResponse(result);
        }
        [Authorize(Policy = "nonAdministratorPolicy")]
        [HttpGet("{touristId:long}/{tourId:long}")]
        public ActionResult<Boolean> ReviewExists(long touristId, long tourId)
        {
            var result = _reviewService.ReviewExists(touristId, tourId);
            return result.Value;
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPost]
        public ActionResult<ReviewResponseDto> Create([FromBody] ReviewCreateDto review)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            { 
              review.TouristId = long.Parse(identity.FindFirst("id").Value);  
            }
            review.CommentDate = DateOnly.FromDateTime(DateTime.Now);
            var result = _reviewService.Create(review);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<ReviewResponseDto> Update([FromBody] ReviewUpdateDto review)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                if (long.Parse(identity.FindFirst("id").Value) != review.TouristId)
                    return Forbid();
            }
            var result = _reviewService.Update(review);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _reviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}
