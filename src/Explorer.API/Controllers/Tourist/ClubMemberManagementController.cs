using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/tourist/club/members")]
public class ClubMemberManagementController : BaseApiController
{
    private readonly IClubMemberManagementService _clubMemberManagementService;

    public ClubMemberManagementController(IClubMemberManagementService clubMemberManagementService)
    {
        _clubMemberManagementService = clubMemberManagementService;
    }

    [HttpDelete("kick/{id:int}")]
    public ActionResult KickTourist(int id)
    {
        var userId = extractUserIdFromHttpContext();
        var result = _clubMemberManagementService.KickTourist(id, userId);
        return CreateResponse(result);
    }

    [HttpGet("{id:long}")]
    public ActionResult GetMembers(long id)
    {
        var members = _clubMemberManagementService.GetMembers(id);
        return CreateResponse(members);
    }

    private long extractUserIdFromHttpContext()
    {
        return long.Parse((HttpContext.User.Identity as ClaimsIdentity).FindFirst("id").Value);
    }
}
