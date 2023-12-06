using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/bundles")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BundleResponseDto>> GetForAuthor()
        {
            var userId = getUserIdByToken();
            var result = _bundleService.GetByAuthor(userId);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BundleResponseDto> Create([FromBody] BundleCreationDto dto)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _bundleService.Create(dto, userId);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<BundleResponseDto> Edit(long id, [FromBody] BundleEditDto dto)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _bundleService.Edit(id, dto, userId);
            return CreateResponse(result);
        }

        [HttpPatch("publish/{id:long}")]
        public ActionResult<BundleResponseDto> Publish(long id)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _bundleService.Publish(id, userId);
            return CreateResponse(result);
        }

        [HttpPatch("archive/{id:long}")]
        public ActionResult<BundleResponseDto> Archive(long id)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _bundleService.Archive(id, userId);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult<BundleResponseDto> Delete(long id)
        {
            long userId = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _bundleService.Delete(id, userId);
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
