using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist.MarketPlace
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/bundles")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BundleResponseDto>> GetPublished()
        {
            var userId = getUserIdByToken();
            var result = _bundleService.GetPublished(userId);
            return CreateResponse(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<BundleResponseDto> GetById(long id)
        {
            var result = _bundleService.GetById(id);
            return CreateResponse(result);
        }

        private long getUserIdByToken()
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            return userId;
        }
    }
}
