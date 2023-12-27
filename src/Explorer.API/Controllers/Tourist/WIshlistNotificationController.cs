using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/wishlist-notification")]
    public class WIshlistNotificationController: BaseApiController
    {
        private readonly IWishlistNotificationService _wishlistNotificationService;

        public WIshlistNotificationController(IWishlistNotificationService wishlistNotificationService)
        {
            _wishlistNotificationService = wishlistNotificationService;
        }

        [HttpGet]
        public ActionResult<List<WishlistNotificationResponseDto>> GetByTouristId()
        {
            var result = _wishlistNotificationService.GetByTouristId(ExtractUserIdFromHttpContext());
            return CreateResponse(result);
        }

        private long ExtractUserIdFromHttpContext()
        {
            return long.Parse((HttpContext.User.Identity as ClaimsIdentity).FindFirst("id").Value);
        }
    }
}
