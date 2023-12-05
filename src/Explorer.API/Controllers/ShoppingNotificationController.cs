using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shoppingNotifications")]
    public class ShoppingNotificationController : BaseApiController
    {
        private readonly IShoppingNotificationService _notificationService;

        public ShoppingNotificationController(IShoppingNotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        public ActionResult<ShoppingNotificationResponseDto> GetNotificationsByTouristId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long id;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument));
            }
            var result = _notificationService.GetByTouristId(page, pageSize, id);
            return CreateResponse(result);
        }
        [HttpGet("set-seen/{notificationId:long}")]
        public ActionResult<ShoppingNotificationResponseDto> SetSeenStatus(long notificationId)
        {
            var result = _notificationService.SetSeenStatus(notificationId);
            return CreateResponse(result);
        }
    }
}
