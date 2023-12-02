using Explorer.Payments.API.Public;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    //[Authorize(Policy = "touristPolicy")] mozda treba staviti nonAdminPolicy provjeriti
    [Route("api/coupon")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

    }

}