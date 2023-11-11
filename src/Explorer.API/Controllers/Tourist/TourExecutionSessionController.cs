using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourexecution/tourexecution")]
    public class TourExecutionSessionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionSessionController(ITourExecutionService tourExecutionService)
        {
            _tourExecutionService = tourExecutionService;
        }
        [HttpPost]
        [Route("")]
        public ActionResult<PagedResult<ClubResponseDto>> StartTour(long tourId)
        {
            // treba provera da li je tura kupljena
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId = long.Parse(identity.FindFirst("id").Value);
            var result = _tourExecutionService.StartTour(tourId, touristId);
            return CreateResponse(result);
        }
        [HttpPut]
        [Route("abandoning")]
        public ActionResult<PagedResult<ClubResponseDto>> AbandonTour(long tourId)
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
        public ActionResult<PagedResult<ClubResponseDto>> CompleteKeyPoint(long tourId)
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
    }
}
