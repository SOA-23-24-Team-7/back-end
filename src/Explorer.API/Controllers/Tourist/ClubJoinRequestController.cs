using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
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
        public ActionResult<ClubJoinRequestSendDto> Send([FromBody] ClubJoinRequestSendDto request)
        {
            var result = _requestService.Send(request);
            return CreateResponse(result);
        }

        [HttpPatch("{id:long}")]
        public ActionResult Respond(long id, [FromBody] ClubJoinRequestResponseDto response)
        {
            var result = _requestService.Respond(id, response);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Cancel(long id)
        {
            var result = _requestService.Cancel(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<PagedResult<ClubJoinRequestByTouristDto>> GetAllByTourist([FromQuery] long touristId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _requestService.GetPagedByTourist(touristId, page, pageSize);
            return CreateResponse(result);
        }
    }
}
