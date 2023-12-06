using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/social-encounter")]
    public class SocialEncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ITouristProgressService _touristProgressService;
        public SocialEncounterController(IEncounterService encounterService, ITouristProgressService touristProgressService)
        {
            _encounterService = encounterService;
            _touristProgressService = touristProgressService;
        }

        [HttpPost("create")]
        public ActionResult<EncounterResponseDto> Create([FromBody] SocialEncounterCreateDto encounter)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var progress = _touristProgressService.GetByUserId(userId);
            if (progress.Value.Level >= 10)
            {
                var result = _encounterService.CreateSocialEncounter(encounter);
                return CreateResponse(result);
            }
            return CreateResponse(Result.Fail("Tourist level is not high enough."));
        }
    }
}
