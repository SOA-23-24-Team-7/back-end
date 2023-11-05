using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var result = _requestService.Create(request);
            return CreateResponse(result);
        }
    }
}
