using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/club-join-request")]
    public class ClubJoinRequestController : BaseApiController
    {
        private readonly IClubJoinRequestService _requestService;

        public ClubJoinRequestController(IClubJoinRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public ActionResult<ClubJoinRequestDto> Send([FromBody] ClubJoinRequestDto request)
        {
            var result = _requestService.Send(request);
            return CreateResponse(result);
        }

        [HttpPatch("respond/{id:long}")]
        public ActionResult Respond(long id, [FromBody] ClubJoinRequestResponseDto response)
        {
            var result = _requestService.Respond(id, response);
            return CreateResponse(result);
        }

        [HttpPatch("cancel/{id:long}")]
        public ActionResult Cancel(long id)
        {
            var result = _requestService.Cancel(id);
            return CreateResponse(result);
        }
    }
}
