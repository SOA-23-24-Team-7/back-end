using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/subscriber")]
    public class SubscriberController : BaseApiController
    {
        private readonly ISubscriberService _subscriberService;
        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpPost]
        public ActionResult<SubscriberResponseDto> SaveCampaign(SubscriberCreateDto createDto)
        {
            var result = _subscriberService.SaveOrUpdate(createDto);
            if (result == null)
            {
                return BadRequest();
            }
            return CreateResponse(result);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<SubscriberResponseDto> GetAll()
        {
            var result = _subscriberService.GetPaged(0,0);
            
            return CreateResponse(result);
        }

        [HttpGet("by-user/{userId:int}")]
        public ActionResult<SubscriberResponseDto> Get(int userId)
        {
            var result = _subscriberService.GetByUserId(userId);
            return CreateResponse(result);
        }
    }
}
