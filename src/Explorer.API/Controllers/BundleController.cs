using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers
{
    [Route("api/bundles")]
    public class BundleController : BaseApiController
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
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

        [HttpPatch("publish/{id:long}")]
        public ActionResult<string> Publish(long id)
        {
            return null;
        }

        [HttpPatch("delete/{id:long}")]
        public ActionResult<string> Delete(long id)
        {
            return null;
        }

        [HttpPatch("archive/{id:long}")]
        public ActionResult<string> Archive(long id)
        {
            return null;
        }

        [HttpPatch("add-items/{id:long}")]
        public ActionResult<string> AddItems(long id, [FromBody] string tourIds)
        {
            return null;
        }

        [HttpPatch("remove-items/{id:long}")]
        public ActionResult<string> RemoveItems(long id, [FromBody] string tourIds)
        {
            return null;
        }
    }
}
