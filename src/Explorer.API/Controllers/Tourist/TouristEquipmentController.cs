using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/equipment")]
    public class TouristEquipmentController : BaseApiController
    {
        private readonly ITouristEquipmentService _touristEquipmentService;
        public TouristEquipmentController(ITouristEquipmentService touristEquipmentService)
        {
            _touristEquipmentService = touristEquipmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TouristEquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _touristEquipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TouristEquipmentDto> Create([FromBody] TouristEquipmentDto touristEquipment)
        {
            var result = _touristEquipmentService.Create(touristEquipment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TouristEquipmentDto> Update([FromBody] TouristEquipmentDto touristEquipment)
        {
            var result = _touristEquipmentService.Update(touristEquipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _touristEquipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
