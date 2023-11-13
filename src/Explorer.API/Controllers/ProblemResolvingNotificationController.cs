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
    }
}
