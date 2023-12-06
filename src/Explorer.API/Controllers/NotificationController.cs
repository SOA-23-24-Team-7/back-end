using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Authorize(Policy = "nonAdministratorPolicy")]
[Route("api/notifications")]
public class NotificationController : BaseApiController
{
    private readonly IProblemResolvingNotificationService notificationService;
    private readonly IShoppingNotificationService shoppingNotificationService;

    public NotificationController(IProblemResolvingNotificationService notificationService, IShoppingNotificationService shoppingNotificationService)
    {
        this.notificationService = notificationService;
        this.shoppingNotificationService = shoppingNotificationService;
    }

    [HttpGet("count")]
    public ActionResult<int> CountUnseenNotifications()
    {
        long loggedInUserId = long.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
        var result = notificationService.CountUnseenNotifications(loggedInUserId);
        return CreateResponse(result);
    }
}