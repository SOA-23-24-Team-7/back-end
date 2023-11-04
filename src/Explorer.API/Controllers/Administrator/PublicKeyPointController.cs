using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/publicKeyPoint")]
    public class PublicKeyPointController : BaseApiController
    {
        private readonly IPublicKeyPointService _publicKeyPointService;

        public PublicKeyPointController(IPublicKeyPointService publicKeyPointService)
        {
            _publicKeyPointService = publicKeyPointService;
        }

        [HttpGet]
        public ActionResult<PagedResult<PublicKeyPointResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _publicKeyPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<PublicKeyPointResponseDto> Create([FromBody] PublicKeyPointCreateDto publicKeyPoint)
        {
            var result = _publicKeyPointService.Create(publicKeyPoint);
            return CreateResponse(result);
        }
    }
}
