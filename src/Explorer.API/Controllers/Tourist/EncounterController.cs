using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Tours.API.Dtos.TouristPosition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounter")]
    public class EncounterController : BaseApiController
    {
        private readonly IEncounterService _encounterService;
        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpPost("{id:long}/activate")]
        public ActionResult<EncounterResponseDto> Activate([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.ActivateEncounter(userId, id, position.Longitude, position.Latitude);
            return CreateResponse(result);
        }

        [HttpPost("{id:long}/complete")]
        public ActionResult<EncounterResponseDto> Activate(long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteEncounter(userId, id);
            return CreateResponse(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<EncounterResponseDto> Get(long id)
        {
            var result = _encounterService.Get(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<EncounterResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("in-range-of")]
        public ActionResult<PagedResult<EncounterResponseDto>> GetAllInRangeOf([FromBody] UserPositionWithRangeDto position, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetAllInRangeOf(position.Range, position.Longitude, position.Latitude, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("active")]
        public ActionResult<PagedResult<EncounterResponseDto>> GetActive([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetActive(page, pageSize);
            return CreateResponse(result);
        }
    }
}
