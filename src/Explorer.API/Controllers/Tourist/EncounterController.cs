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
        private readonly ITouristProgressService _progressService;
        public EncounterController(IEncounterService encounterService, ITouristProgressService progressService)
        {
            _encounterService = encounterService;
            _progressService = progressService;
        }

        [HttpGet("{encounterId:long}/instance")]
        public ActionResult<EncounterResponseDto> GetInstance(long encounterId)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.GetInstance(userId, encounterId);
            return CreateResponse(result);
        }

        [HttpPost("{id:long}/activate")]
        public ActionResult<EncounterResponseDto> Activate([FromBody] TouristPositionCreateDto position, long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.ActivateEncounter(userId, id, position.Longitude, position.Latitude);
            return CreateResponse(result);
        }

        [HttpPost("{id:long}/complete")]
        public ActionResult<EncounterResponseDto> Complete(long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CompleteEncounter(userId, id);
            return CreateResponse(result);
        }

        [HttpGet("{id:long}/cancel")]
        public ActionResult<EncounterResponseDto> Cancel(long id)
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _encounterService.CancelEncounter(userId, id);
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


        [HttpGet("done-encounters")]
        public ActionResult<PagedResult<EncounterResponseDto>> GetAllDoneByUser(int currentUserId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetAllDoneByUser(currentUserId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("active")]
        public ActionResult<PagedResult<EncounterResponseDto>> GetActive([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetActive(page, pageSize);
            return CreateResponse(result);
        }


        [HttpPost("key-point/{keyPointId:long}")]
        public ActionResult<KeyPointEncounterResponseDto> ActivateKeyPointEncounter(
            [FromBody] TouristPositionCreateDto position, long keyPointId)
        {
            long userId = int.Parse(HttpContext.User.Claims
                .First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result =
                _encounterService.ActivateKeyPointEncounter(position.Longitude, position.Latitude, keyPointId, userId);

            return CreateResponse(result);
        }

        [HttpGet("progress")]
        public ActionResult<TouristProgressResponseDto> GetProgress()
        {
            long userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _progressService.GetByUserId(userId);

            return CreateResponse(result);
        }
    }
}
