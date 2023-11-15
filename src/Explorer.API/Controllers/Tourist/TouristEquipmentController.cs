using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.TouristEquipment;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TouristEquipment;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/equipment")]
    public class TouristEquipmentController : BaseApiController
    {
        private readonly ITouristEquipmentService _touristEquipmentService;
        private readonly IEquipmentService _equipmentService;
        public TouristEquipmentController(ITouristEquipmentService touristEquipmentService, IEquipmentService equipmentService)
        {
            _touristEquipmentService = touristEquipmentService;
            _equipmentService = equipmentService;
        }

        [HttpGet("/api/tourist/only_equipment")]
        public ActionResult<PagedResult<EquipmentResponseDto>> GetAllEquipment([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<PagedResult<TouristEquipmentResponseDto>> GetAllTouristEquipment([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _touristEquipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TouristEquipmentResponseDto> Create([FromBody] TouristEquipmentCreateDto touristEquipment)
        {
            int touristId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            touristEquipment.TouristId = touristId;
            if (GetForTourist(touristId) != null)
            {
                return BadRequest();
            }
            var result = _touristEquipmentService.Create(touristEquipment);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<TouristEquipmentResponseDto> Update([FromBody] TouristEquipmentUpdateDto touristEquipment)
        {
            int touristId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            if (touristEquipment.TouristId != touristId)
            {
                return Unauthorized();
            }
            var result = _touristEquipmentService.Update(touristEquipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            int touristId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            if (GetForTourist(touristId) == null || GetForTourist(touristId)?.Id != id)
            {
                return Unauthorized();
            }
            var result = _touristEquipmentService.Delete(id);
            return CreateResponse(result);
        }

        private TouristEquipmentResponseDto GetForTourist(int touristId)
        {
            return ((GetAllTouristEquipment(0, 0).Result as OkObjectResult).Value as PagedResult<TouristEquipmentResponseDto>).Results.Find(te => te.TouristId == touristId);
        }
    }
}
