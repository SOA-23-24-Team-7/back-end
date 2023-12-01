using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Authorize(Policy = "nonAdministratorPolicy")]
[Route("api/notifications")]
public class NotificationController : BaseApiController
{
    private readonly IProblemResolvingNotificationService notificationService;

    public NotificationController(IProblemResolvingNotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    [HttpGet("count")]
    public ActionResult<int> CountUnseenNotifications()
    {
        long loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
        var result = notificationService.CountUnseenNotifications(loggedInUserId);
        return CreateResponse(result);
    }
}