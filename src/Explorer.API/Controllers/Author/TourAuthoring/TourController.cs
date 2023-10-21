using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.TourAuthoring;

[Authorize(Policy = "authorPolicy")]
[Route("api/tour-authoring/tours")]
public class TourController : BaseApiController
{
    private readonly IKeyPointService _keyPointService;

    public TourController(IKeyPointService keyPointService)
    {
        _keyPointService = keyPointService;
    }

    [HttpGet("{tourId:long}/key-points")]
    public ActionResult<List<KeyPointDto>> GetKeyPoints(long tourId)
    {
        var result = _keyPointService.GetByTourId(tourId);
        return CreateResponse(result);
    }

    [HttpPost("{tourId:long}/key-points")]
    public ActionResult<KeyPointDto> CreateKeyPoint([FromRoute] long tourId, [FromBody] KeyPointDto keyPoint)
    {
        keyPoint.TourId = tourId;
        var result = _keyPointService.Create(keyPoint);
        return CreateResponse(result);
    }
}
