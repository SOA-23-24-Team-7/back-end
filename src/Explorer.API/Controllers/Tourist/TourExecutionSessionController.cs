using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System.Collections.Generic;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourexecution/tourexecution")]
    public class TourExecutionSessionController : BaseApiController
    {
        private readonly ITourExecutionSessionService _tourExecutionService;
        private readonly ITourService _tourService;
        private readonly ITourTokenService _tourTokenService;

        public TourExecutionSessionController(ITourExecutionSessionService tourExecutionService, ITourService tourService, ITourTokenService tourTokenService)
        {
            _tourExecutionService = tourExecutionService;
            _tourService = tourService;
            _tourTokenService = tourTokenService;
        }

        [HttpGet]
        [Route("purchasedtours")]
        public ActionResult<PagedResult<TourResponseDto>> GetPurchasedTours()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            touristId = long.Parse(identity.FindFirst("id").Value);
            var purchasedTourIds = _tourTokenService.GetTouristToursId(touristId).Value;
            var result = _tourService.GetTours(purchasedTourIds);
            var temp = CreateResponse(result);
            return temp;
        }

        [HttpGet("{tourId:long}")]
        public ActionResult<PagedResult<TourResponseDto>> GetById(long tourId)
        {
            var result = _tourService.GetById(tourId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourExecutionSessionResponseDto> StartTour(TourExecutionDto executionDto)
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
            var result = _tourExecutionService.StartTour(executionDto.TourId, executionDto.IsCampaign, touristId);
            return CreateResponse(result);
        }

        [HttpPut]
        [Route("abandoning")]
        public ActionResult<TourExecutionSessionResponseDto> AbandonTour(TourExecutionDto executionDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            if (identity != null && identity.IsAuthenticated)
                touristId = long.Parse(identity.FindFirst("id").Value);
            // za potrebe testiranja
            else
                touristId = -21;
            var result = _tourExecutionService.AbandonTour(executionDto.TourId, executionDto.IsCampaign, touristId);
            if (result == null)
            {
                return BadRequest();
            }
            return CreateResponse(result);
        }

        [HttpPut]
        [Route("{tourId:long}/{isCampaign:bool}/keypoint")]
        public ActionResult<TourExecutionSessionResponseDto> CompleteKeyPoint(long tourId, bool isCampaign, TouristPositionResponseDto touristPosition)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            if (identity != null && identity.IsAuthenticated)
                touristId = long.Parse(identity.FindFirst("id").Value);
            // za potrebe testiranja
            else
                touristId = -21;
            var result = _tourExecutionService.CheckKeyPointCompletion(tourId, touristId, touristPosition.Longitude, touristPosition.Latitude, isCampaign);
            if (result == null)
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
            if (result == null)
            {
                return NoContent();
            }
            return CreateResponse(result);
        }
    }
}
