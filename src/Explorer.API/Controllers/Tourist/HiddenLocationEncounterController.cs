using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos.TouristPosition;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/hidden-location-encounter")]
    public class HiddenLocationEncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        private readonly ITouristProgressService _touristProgressService;
        public HiddenLocationEncounterController(IEncounterService encounterService, ITouristProgressService touristProgressService)
        {
            _encounterService = encounterService;
            _touristProgressService = touristProgressService;
        }

        [HttpGet("{id:long}")]
        public ActionResult<EncounterResponseDto> GetHiddenLocationEncounterById(long id)
        {
            var result = _encounterService.GetHiddenLocationEncounterById(id);
            return CreateResponse(result);
        }

        [HttpPost("{id:long}/complete")]
        public ActionResult<EncounterResponseDto> Complete([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteHiddenLocationEncounter(userId, id, position.Longitude, position.Latitude);
            return CreateResponse(result);
        }

        [HttpPost("{id:long}/check-range")]
        public bool CheckIfUserInCompletionRange([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            return _encounterService.CheckIfUserInCompletionRange(userId, id, position.Longitude, position.Latitude);
        }

        [HttpPost("create")]
        public ActionResult<HiddenLocationEncounterResponseDto> Create([FromBody] HiddenLocationEncounterCreateDto encounter)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var progress = _touristProgressService.GetByUserId(userId);
            if (progress.Value.Level >= 10)
            {
                var result = _encounterService.CreateHiddenLocationEncounter(encounter);
                return CreateResponse(result);

            }
            return CreateResponse(Result.Fail("Tourist level is not high enough."));
        }
    }
}
