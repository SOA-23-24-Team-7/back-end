using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "userPolicy")]
    [Route("api/notifications/problems")]
    public class ProblemResolvingNotificationController : BaseApiController
    {
        private readonly IProblemResolvingNotificationService _notificationService;
        public ProblemResolvingNotificationController(IProblemResolvingNotificationService problemResolvingNotificationService)
        {
            _notificationService = problemResolvingNotificationService;
        }

        [HttpGet]
        public ActionResult<ProblemResolvingNotificationResponseDto> GetByLoggedInUser(int page, int pageSize)
        {
            var loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _notificationService.GetByLoggedInUser(page, pageSize, loggedInUserId);
            return CreateResponse(result);
        }

        [HttpGet("set-seen/{notificationId:long}")]
        public ActionResult<ProblemResolvingNotificationResponseDto> SetSeenStatus(long notificationId)
        {
            var result = _notificationService.SetSeenStatus(notificationId);
            return CreateResponse(result);
        }
    }
}
