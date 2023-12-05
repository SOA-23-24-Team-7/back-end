using System.Security.Claims;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    //[Authorize(Policy = "authorPolicy")] //mozda treba staviti nonAdminPolicy provjeriti
    [Route("api/coupon")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpPost]
        public ActionResult<CouponResponseDto> Create([FromBody] CouponCreateDto coupon)
        {
            var result = _couponService.Create(coupon);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<CouponResponseDto> Update([FromBody] CouponUpdateDto coupon)
        {
            var result = _couponService.Update(coupon);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _couponService.Delete(id);
            return CreateResponse(result);
        }
        [HttpGet]
        public ActionResult<PagedResult<ProblemResponseDto>> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _couponService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
    }

}