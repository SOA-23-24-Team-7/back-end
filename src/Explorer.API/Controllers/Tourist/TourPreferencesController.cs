using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-preferences")]
    public class TourPreferencesController : BaseApiController
    {
        private readonly ITourPreferencesService _tourPreferencesService;

        public TourPreferencesController(ITourPreferencesService tourPreferencesService)
        {
            _tourPreferencesService = tourPreferencesService;
        }

        [HttpGet]
        public ActionResult<TourPreferencesDto> GetTourPreference([FromQuery] int id)
        {
            var result = _tourPreferencesService.GetByUserId(id);
            return CreateResponse(result);
        }

        [HttpPost("create")]
        public ActionResult<TourPreferencesDto> CreateTourPreference([FromBody] TourPreferencesDto preferences)
        {
            var result = _tourPreferencesService.Create(preferences);
            return CreateResponse(result);
        }
    }
}
