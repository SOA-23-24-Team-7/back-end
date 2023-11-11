using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourexecution/tourexecution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService)
        {
            _tourExecutionService = tourExecutionService;
        }
        [HttpPost]
        [Route("")]
        public ActionResult<PagedResult<TourExecutionResponseDto>> StartTour(long tourId)
        {
            // treba provera da li je tura kupljena
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId = long.Parse(identity.FindFirst("id").Value);
            if(_tourExecutionService.GetLive(touristId) != null)
            {
                return Conflict();
            }
            var result = _tourExecutionService.StartTour(tourId, touristId);
            return CreateResponse(result);
        }
        [HttpPut]
        [Route("abandoning")]
        public ActionResult<PagedResult<TourExecutionResponseDto>> AbandonTour(long tourId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId = long.Parse(identity.FindFirst("id").Value);
            var result = _tourExecutionService.AbandonTour(tourId, touristId);
            if(result == null)
            {
                return BadRequest();
            }
            return CreateResponse(result);
        }
        [HttpPut]
        [Route("keypoint")]
        public ActionResult<PagedResult<TourExecutionResponseDto>> CompleteKeyPoint(long tourId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId = long.Parse(identity.FindFirst("id").Value);
            var result = _tourExecutionService.CompleteKeyPoint(tourId, touristId);
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
            long touristId = long.Parse(identity.FindFirst("id").Value);
            var result = _tourExecutionService.GetAllFor(touristId);
            return CreateResponse(result);
        }
        [HttpGet]
        [Route("live")]
        public ActionResult<PagedResult<TourResponseDto>> GetLiveTour()
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
