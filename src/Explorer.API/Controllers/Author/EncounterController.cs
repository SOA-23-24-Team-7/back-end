using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/author/encounter")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    public EncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [HttpPost]
    public ActionResult CreateKeyPointEncounter(KeyPointEncounterCreateDto keyPointEncounter)
    {
        long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
        var result = _encounterService.CreateKeyPointEncounter(keyPointEncounter, userId);
        return CreateResponse(result);
    }
}