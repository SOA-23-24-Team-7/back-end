using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/tourist/social-encounter")]
public class SocialEncounterController : BaseApiController
{
    private readonly ISocialEncounterService _socialEncounterService;
    public SocialEncounterController(ISocialEncounterService socialEncounterService)
    {
        _socialEncounterService = socialEncounterService;
    }

    [Authorize(Policy = "touristPolicy")]
    [HttpPost("complete")]
    public ActionResult<SocialEncounterCreateDto> Complete([FromBody] SocialEncounterCompleteDto encounterCompleteDto)
    {
        try
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _socialEncounterService.CompleteSocialEncounter(userId, encounterCompleteDto.EncounterId);
            return CreateResponse(result);
        }
        catch (Exception e) 
        {
            throw e;
        }
    }

    [Authorize(Policy = "authorPolicy")]
    [HttpPost]
    public ActionResult<SocialEncounterResponseDto> Create([FromBody] SocialEncounterCreateDto encounter)
    {
        var result = _socialEncounterService.Create(encounter);
        return CreateResponse(result);
    }
}
