using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.TourAuthoring;

[Authorize(Policy = "authorPolicy")]
[Route("api/tour-authoring")]
public class KeyPointController : BaseApiController
{
    private readonly IKeyPointService _keyPointService;

    public KeyPointController(IKeyPointService keyPointService)
    {
        _keyPointService = keyPointService;
    }

    [HttpPost("tours/{tourId:long}/key-points")]
    public ActionResult<KeyPointDto> CreateKeyPoint([FromRoute] long tourId, [FromBody] KeyPointDto keyPoint)
    {
        keyPoint.TourId = tourId;
        var result = _keyPointService.Create(keyPoint);
        return CreateResponse(result);
    }

    [HttpPut("tours/{tourId:long}/key-points/{id:long}")]
    public ActionResult<KeyPointDto> Update(long tourId, long id, [FromBody] KeyPointDto keyPoint)
    {
        keyPoint.Id = id;
        var result = _keyPointService.Update(keyPoint);
        return CreateResponse(result);
    }

    [HttpDelete("tours/{tourId:long}/key-points/{id:long}")]
    public ActionResult Delete(long tourId, long id)
    {
        var result = _keyPointService.Delete(id);
        return CreateResponse(result);
    }
}
