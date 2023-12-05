using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpPost]
        [Route("api/author/social-encounter/create")]
        public ActionResult<EncounterResponseDto> Create([FromBody] SocialEncounterCreateDto encounter)
        {
            var result = _encounterService.CreateSocialEncounter(encounter);
            return CreateResponse(result);
        }
    }
}
