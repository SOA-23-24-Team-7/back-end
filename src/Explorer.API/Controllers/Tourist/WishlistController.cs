using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
<<<<<<< HEAD
using FluentResults;
=======
using Explorer.Payments.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
>>>>>>> d901044fd0482460c9a1e5c4ed7bf8172adaf944
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
        private readonly ITourService _tourService;

        public WishlistController(IWishlistService wishlistService, ITourService tourService)
        {
            _wishlistService = wishlistService;
            _tourService = tourService;
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

<<<<<<< HEAD
        [HttpDelete("{wishlistId:long}")]
        public void RemoveTourFromWishlist(long wishlistId)
        {
            _wishlistService.RemoveTourFromWishlist(wishlistId);
        }
=======
        [HttpGet]
        [Route("tourist")]
        public ActionResult<PagedResult<TourResponseDto>> GetPurchasedTours()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            touristId = long.Parse(identity.FindFirst("id").Value);
            var purchasedTourIds = _wishlistService.GetTouristToursId(touristId).Value;
            var result = _tourService.GetTours(purchasedTourIds);
            var temp = CreateResponse(result);
            return temp;
        }

        [HttpDelete("{tourId:long}")]
        public ActionResult RemoveTourFromWishlist(long tourId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId;
            touristId = long.Parse(identity.FindFirst("id").Value);
            var result = _wishlistService.RemoveTourFromWishlist(tourId,touristId);
            return CreateResponse(result);
        }

>>>>>>> d901044fd0482460c9a1e5c4ed7bf8172adaf944
    }
}
