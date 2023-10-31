using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/requests")]
    public class RequestManagingController : BaseApiController
    {
        private readonly IPublicKeyPointRequestService _publicKeyPointRequestService;
        private readonly IPublicFacilityRequestService _publicFacilityRequestService;
        public RequestManagingController(IPublicKeyPointRequestService publicKeyPointRequestService, IPublicFacilityRequestService publicFacilityRequestService)
        {
            _publicKeyPointRequestService = publicKeyPointRequestService;
            _publicFacilityRequestService = publicFacilityRequestService;
        }
        [HttpGet]
        public ActionResult<PagedResult<PublicKeyPointRequestResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicKeyPointRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPut("{id:long}")]
        public ActionResult<PublicKeyPointRequestResponseDto> Update([FromBody] PublicKeyPointRequestUpdateDto response)
        {
            var result = _publicKeyPointRequestService.Update(response);
            return CreateResponse(result);
        }
        [HttpGet("facility")]
        public ActionResult<PagedResult<PublicFacilityRequestResponseDto>> GetAllFacilityRequest([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicFacilityRequestService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPut("facility/{id:long}")]
        public ActionResult<PublicFacilityRequestResponseDto> UpdateFacility([FromBody] PublicFacilityRequestUpdateDto response)
        {
            var result = _publicFacilityRequestService.Update(response);
            return CreateResponse(result);
        }
    }
}
