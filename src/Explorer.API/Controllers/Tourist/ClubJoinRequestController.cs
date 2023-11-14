using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public ActionResult<ClubJoinRequestCreatedDto> Send([FromBody] ClubJoinRequestSendDto request)
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

        [HttpGet("tourist")]
        public ActionResult<PagedResult<ClubJoinRequestByTouristDto>> GetAllByTourist([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long touristId = long.Parse(identity.FindFirst("id").Value);
            var result = _requestService.GetPagedByTourist(touristId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("club/{id:long}")]
        public ActionResult<PagedResult<ClubJoinRequestByClubDto>> GetAllByClub(long id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _requestService.GetPagedByClub(id, page, pageSize);
            return CreateResponse(result);
        }
    }
}
