using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/facility/")]
    public class FacilityController: BaseApiController
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet("public")]
        public ActionResult GetPublic()
        {
            var result = _facilityService.GetPublic();
            return CreateResponse(result);
        }
        
    }
}
