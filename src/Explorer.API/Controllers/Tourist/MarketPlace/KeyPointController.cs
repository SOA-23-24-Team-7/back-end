using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.MarketPlace
{
    [Route("api/market-place")]
    public class KeyPointController : BaseApiController
    {
        private readonly IKeyPointService _keyPointService;

        public KeyPointController(IKeyPointService keyPointService)
        {
            _keyPointService = keyPointService;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/{tourId:long}/key-points")]
        public ActionResult<KeyPointResponseDto> GetKeyPoints(long tourId)
        {
            var result = _keyPointService.GetByTourId(tourId);
            return CreateResponse(result);
        }
        [Authorize(Roles = "tourist")]
        [HttpGet("{campaignId:long}/key-points")]
        public ActionResult<KeyPointResponseDto> GetCampaignKeyPoints(long campaignId)
        {
            var result = _keyPointService.GetByCampaignId(campaignId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/{tourId:long}/firts-key-point")]
        public ActionResult<KeyPointResponseDto> GetToursFirstKeyPoint(long tourId)
        {
            var result = _keyPointService.GetFirstByTourId(tourId);
            return CreateResponse(result);
        }
    }
}
