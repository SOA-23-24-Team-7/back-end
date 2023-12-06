using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/bundle-records")]
    public class BundleRecordController : BaseApiController
    {
        private readonly IBundleRecordService _bundleRecordService;

        public BundleRecordController(IBundleRecordService bundleRecordService)
        {
            _bundleRecordService = bundleRecordService;
        }

        [HttpGet]
        public ActionResult<List<BundleRecordResponseDto>> GetByTourist()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long id;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument));
            }
            var result = _bundleRecordService.GetAllByTourist(id);
            return CreateResponse(result);
        }
    }
}
