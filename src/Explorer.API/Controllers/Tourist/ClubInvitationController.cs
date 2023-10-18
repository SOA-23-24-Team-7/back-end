using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/club/invite")]
    public class ClubInvitationController : BaseApiController
    {
        private readonly IClubInvitationService _clubInvitationService;

        public ClubInvitationController(IClubInvitationService clubInvitationService)
        {
            _clubInvitationService = clubInvitationService;
        }

        [HttpPost]
        public ActionResult<ClubInvitationDto> Invite([FromBody] ClubInvitationDto dto)
        {
            var result = _clubInvitationService.InviteTourist(dto);
            return CreateResponse(result);
        }

        [HttpPatch("reject/{clubInvitationId:long}")]
        public ActionResult Reject(long clubInvitationId)
        {
            var result = _clubInvitationService.Reject(clubInvitationId);
            return CreateResponse(result);
        }

        [HttpPatch("accept/{clubInvitationId:long}")]
        public ActionResult Accept(long clubInvitationId)
        {
            var result = _clubInvitationService.Accept(clubInvitationId);
            return CreateResponse(result);
        }
    }
}
