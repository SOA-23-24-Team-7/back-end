using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.TourAuthoring;
[Authorize(Policy = "authorPolicy")]
    [Route("api/author/publicKeyPoint")]
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

        [HttpPost("addPrivate/{tourId:int}/{publicKeyPointId:int}")]
        public ActionResult<KeyPointCreateDto> CreatePrivateKeyPoint(int tourId, int publicKeyPointId)
        {
            var result = _publicKeyPointService.CreatePrivateKeyPoint(tourId, publicKeyPointId);
            return CreateResponse(result);
        }
    }


