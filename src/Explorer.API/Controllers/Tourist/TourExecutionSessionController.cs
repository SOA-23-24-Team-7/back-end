using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourexecution/tourexecution")]
    public class TourExecutionSessionController : BaseApiController
    {
        private readonly ITourExecutionSessionService _tourExecutionService;
        private readonly ITourService _tourService;

        public TourExecutionSessionController(ITourExecutionSessionService tourExecutionService, ITourService tourService)
        {
            _tourExecutionService = tourExecutionService;
            _tourService = tourService;
        }

        [HttpGet]
        [Route("purchasedtours")]
        public ActionResult<PagedResult<TourResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("{tourId:long}")]
        public ActionResult<PagedResult<TourResponseDto>> GetById(long tourId)
        {
            var result = _tourService.GetById(tourId);
            return CreateResponse(result);
        }

        [HttpPost("{tourId:long}")]
        public ActionResult<TourExecutionSessionResponseDto> StartTour(long tourId)
        {
            // treba provera da li je tura kupljena
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            if (identity != null && identity.IsAuthenticated)
                touristId = long.Parse(identity.FindFirst("id").Value);
            // za potrebe testiranja
            else
                touristId = -21;
            if (_tourExecutionService.GetLive(touristId) != null)
            {
                return Conflict();
            }
            var result = _tourExecutionService.StartTour(tourId, touristId);
            return CreateResponse(result);
        }

        [HttpPut]
        [Route("abandoning")]
        public ActionResult<TourExecutionSessionResponseDto> AbandonTour(long tourId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            if (identity != null && identity.IsAuthenticated)
                touristId = long.Parse(identity.FindFirst("id").Value);
            // za potrebe testiranja
            else
                touristId = -21;
            var result = _tourExecutionService.AbandonTour(tourId, touristId);
            if(result == null)
            {
                return BadRequest();
            }
            return CreateResponse(result);
        }

        [HttpPut]
        [Route("{tourId:long}/keypoint")]
        public ActionResult<TourExecutionSessionResponseDto> CompleteKeyPoint(long tourId, TouristPositionResponseDto touristPosition)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            if (identity != null && identity.IsAuthenticated)
                touristId = long.Parse(identity.FindFirst("id").Value);
            // za potrebe testiranja
            else
                touristId = -21;
            var result = _tourExecutionService.CheckKeyPointCompletion(tourId, touristId, touristPosition.Longitude, touristPosition.Latitude);
            if(result == null)
            {
                return BadRequest();
            }
            return CreateResponse(result);
        }
        [HttpGet]
        [Route("allInfo")]
        public ActionResult<PagedResult<TourExecutionInfoDto>> GetExecutedToursInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            if (identity != null && identity.IsAuthenticated)
                touristId = long.Parse(identity.FindFirst("id").Value);
            // za potrebe testiranja
            else
                touristId = -21;
            var result = _tourExecutionService.GetAllFor(touristId);
            return CreateResponse(result);
        }
        [HttpGet]
        [Route("live")]
        public ActionResult<PagedResult<TourExecutionSessionResponseDto>> GetLiveTour()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId = long.Parse(identity.FindFirst("id").Value);
            var result = _tourExecutionService.GetLive(touristId);
            if(result == null)
            {
                return NoContent();
            }
            return CreateResponse(result);
        }
    }
}
