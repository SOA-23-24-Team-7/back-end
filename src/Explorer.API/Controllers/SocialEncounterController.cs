using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Authorize(Policy = "touristPolicy")]
[Route("api/tourist/social-encounter")]
public class SocialEncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    public SocialEncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [HttpPost]
    public ActionResult<SocialEncounterResponseDto> Activate([FromBody] SocialEncounterActivationDto encounter)
    {
        throw new NotImplementedException();
    }
}
