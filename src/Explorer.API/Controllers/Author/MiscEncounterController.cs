using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/misc-encounter")]
    public class MiscEncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public MiscEncounterController(IEncounterService encounterService) {
            _encounterService = encounterService;
        }


        [HttpPost("createMisc")]
        public ActionResult<MiscEncounterResponseDto> Create([FromBody] MiscEncounterCreateDto encounter)
        {
            var result = _encounterService.CreateMiscEncounter(encounter);
            return CreateResponse(result);
        }

    }
}
