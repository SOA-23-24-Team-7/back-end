using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
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
