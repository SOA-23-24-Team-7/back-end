using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist")]
    public class TourController : BaseApiController
    {
        private readonly IKeyPointService _keyPointService;

        public TourController(IKeyPointService keyPointService)
        {
            _keyPointService = keyPointService;
        }

        [HttpGet("tour/{tourId:long}/key-points")]
        public ActionResult<KeyPointDto> GetKeyPoints(long tourId)
        {
            var result = _keyPointService.GetByTourId(tourId);
            return CreateResponse(result);
        }
    }
}
