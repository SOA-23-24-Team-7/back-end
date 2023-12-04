using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/tourist/social-encounter")]
public class SocialEncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    public SocialEncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [Authorize(Policy = "touristPolicy")]
    [HttpPost("complete")]
    public ActionResult<SocialEncounterCreateDto> Complete([FromBody] SocialEncounterCompleteDto encounterCompleteDto)
    {
        try
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteSocialEncounter(userId, encounterCompleteDto.EncounterId);
            return CreateResponse(result);
        }
        catch (Exception e) 
        {
            throw e;
        }
    }
}
