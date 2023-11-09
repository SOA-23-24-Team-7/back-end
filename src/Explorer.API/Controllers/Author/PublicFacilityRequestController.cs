using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Author
{

    [Authorize(Policy = "authorPolicy")]
    [Route("api/publicFacilityRequest")]
    public class PublicFacilityRequestController : BaseApiController
    {
        private readonly IPublicFacilityRequestService _requestService;

        public PublicFacilityRequestController(IPublicFacilityRequestService requestService)
        {
            _requestService = requestService;
        }
        [HttpPost]
        public ActionResult<PublicFacilityRequestResponseDto> Create([FromBody] PublicFacilityRequestCreateDto request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                request.AuthorId = long.Parse(identity.FindFirst("id").Value);
            }
            request.Created = DateTime.UtcNow;
            var result = _requestService.Create(request);
            return CreateResponse(result);
        }
    }
}
