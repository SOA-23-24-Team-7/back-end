using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/social-encounter")]
    public class SocialEncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public SocialEncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpPost("create")]
        public ActionResult<EncounterResponseDto> Create([FromBody] SocialEncounterCreateDto encounter)
        {
            var result = _encounterService.CreateSocialEncounter(encounter);
            return CreateResponse(result);
        }
    }
}
