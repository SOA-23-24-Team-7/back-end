using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist.MarketPlace
{
    [Route("api/market-place")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourController(ITourService service)
        {
            _tourService = service;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/published")]
        public ActionResult<PagedResult<TourResponseDto>> GetPublishedTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPublished(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("tours/{tourId:long}")]
        public ActionResult<PagedResult<TourResponseDto>> GetById(long tourId)
        {
            var result = _tourService.GetById(tourId);
            return CreateResponse(result);
        }

        [HttpGet("tours/can-be-rated/{tourId:long}")]
        public bool CanTourBeRated(long tourId)
        {
            long userId = extractUserIdFromHttpContext();
            return _tourService.CanTourBeRated(tourId, userId).Value;
        }

        private long extractUserIdFromHttpContext()
        {
            return long.Parse((HttpContext.User.Identity as ClaimsIdentity).FindFirst("id").Value);
        }
    }
}
