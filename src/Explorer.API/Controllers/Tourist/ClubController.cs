using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }
        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<ClubDto> Create([FromBody] ClubDto club)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                club.OwnerId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _clubService.Create(club);
            return CreateResponse(result);
        }
        [HttpPut]
        public ActionResult<ClubDto> Update([FromBody] ClubDto club)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                club.OwnerId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _clubService.Update(club);
            return CreateResponse(result);
        }
    }
}
