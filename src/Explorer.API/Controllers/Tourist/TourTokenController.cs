using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/token")]
    public class TourTokenController : BaseApiController
    {
        private readonly ITourTokenService _tokenService;

        public TourTokenController(ITourTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("{tourId:long}/{touristId:long}/{totalPrice:long}/{orderItemPrice:long}")]
        public ActionResult<TourTokenResponseDto> AddToken(long tourId, long touristId,long totalPrice, long orderItemPrice)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            TourTokenCreateDto dto = new TourTokenCreateDto();
            dto.TourId = tourId;
            dto.TouristId = touristId; 
            var result = _tokenService.AddToken(dto, totalPrice, orderItemPrice);
            return CreateResponse(result);
        }

        [HttpGet("tourists")]
        public ActionResult<List<TourTokenResponseDto>> GetTouristsTokens()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long id = 0;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }

            var result = _tokenService.GetTouristsTokens(id);
            return CreateResponse(result);
        }

        [HttpPost("bundle/{bundleId:long}")]
        public ActionResult AddTokensByBundle(long bundleId)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tokenService.AddTokensByBundle(userId, bundleId);
            return CreateResponse(result);
        }
    }
}
