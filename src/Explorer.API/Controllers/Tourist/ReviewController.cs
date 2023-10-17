using System.Security.Claims;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/review")]
    public class ReviewController : BaseApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _reviewService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        [HttpPost]
        public ActionResult<ReviewDto> Create([FromBody] ReviewDto review)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var claimId = identity.FindFirst("id");
                if(claimId != null) {
                    review.TouristId = Int32.Parse(claimId.Value);
                }
                
            }
            review.CommentDate = DateOnly.FromDateTime(DateTime.Now);
            var result = _reviewService.Create(review);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ReviewDto> Update([FromBody] ReviewDto review)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var claimId = identity.FindFirst("id");
                if (claimId != null)
                {
                    review.TouristId = Int32.Parse(claimId.Value);
                }
            }
            var result = _reviewService.Update(review);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _reviewService.Delete(id);
            return CreateResponse(result);
        }
    }
}
