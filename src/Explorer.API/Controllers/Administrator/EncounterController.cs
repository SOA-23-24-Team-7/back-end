using Explorer.Blog.API.Public;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administrator/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpPost]
        public ActionResult<EncounterResponseDto> Create([FromBody] EncounterCreateDto encounter)
        {
            var result = _encounterService.Create(encounter);
            return CreateResponse(result);
        }

    }
}
