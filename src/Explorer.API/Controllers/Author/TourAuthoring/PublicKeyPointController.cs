using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.TourAuthoring;
[Route("api/author/publicKeyPoint")]
public class PublicKeyPointController : BaseApiController
{
    private readonly IPublicKeyPointService _publicKeyPointService;

    public PublicKeyPointController(IPublicKeyPointService publicKeyPointService)
    {
        _publicKeyPointService = publicKeyPointService;
    }

    [Authorize(Roles = "author, tourist")]
    [HttpGet]
    public ActionResult<PagedResult<PublicKeyPointResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _publicKeyPointService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [Authorize(Roles = "author, tourist")]
    [HttpPost("addPrivate/{tourId:int}/{publicKeyPointId:int}")]
    public ActionResult<KeyPointResponseDto> CreatePrivateKeyPoint(int tourId, int publicKeyPointId)
    {
        var result = _publicKeyPointService.CreatePrivateKeyPoint(tourId, publicKeyPointId);
        return CreateResponse(result);
    }
}
