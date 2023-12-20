using Explorer.Tours.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/preferences")]
    public class PreferenceController : BaseApiController
    {
        private readonly IPreferenceService _tourPreferencesService;

        public PreferenceController(IPreferenceService tourPreferencesService)
        {
            _tourPreferencesService = tourPreferencesService;
        }

        [HttpGet]
        public ActionResult<PreferenceResponseDto> Get()
        {
           // int userId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            int id = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                id = int.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourPreferencesService.GetByUserId(id);
            return CreateResponse(result);
        }

        [HttpPost("create")]
        public ActionResult<PreferenceResponseDto> Create([FromBody] PreferenceCreateDto preference)
        {
            preference.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _tourPreferencesService.Create(preference);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPreferencesService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<PreferenceResponseDto> Update([FromBody] PreferenceUpdateDto preference)
        {
            preference.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _tourPreferencesService.Update(preference);
            return CreateResponse(result);
        }
    }
}
