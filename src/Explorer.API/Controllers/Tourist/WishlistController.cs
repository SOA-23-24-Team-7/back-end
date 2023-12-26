using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/wishlist")]
    public class WishlistController : BaseApiController
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost("{tourId:long}")]
        public ActionResult<WishlistResponseDto> AddTourToWishlist(long tourId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = long.Parse(identity.FindFirst("id").Value);
            WishlistCreateDto dto = new WishlistCreateDto();
            dto.TourId = tourId;
            dto.TouristId = id;
            var result = _wishlistService.AddTourToWishlist(dto);
            return CreateResponse(result);
        }

        [HttpDelete("{wishlistId:long}")]
        public void RemoveTourFromWishlist(long wishlistId)
        {
            _wishlistService.RemoveTourFromWishlist(wishlistId);
        }
    }
}
