using Explorer.Tours.API.Dtos.TouristPosition;
using Explorer.Tours.API.Public.TourExecution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourExecution;

[Authorize(Policy = "touristPolicy")]
[Route("api/tour-execution/tourists")]
public class TouristPositionController : BaseApiController
{
    private readonly ITouristPositionService _touristPositionService;

    public TouristPositionController(ITouristPositionService touristPositionService)
    {
        _touristPositionService = touristPositionService;
    }

    [HttpPost("position")]
    public ActionResult<TouristPositionResponseDto> Create([FromBody] TouristPositionCreateDto touristPosition)
    {
        var result = _touristPositionService.Create(touristPosition);
        return CreateResponse(result);
    }

    [HttpPut("position")]
    public ActionResult<TouristPositionResponseDto> Update([FromBody] TouristPositionUpdateDto touristPosition)
    {
        var result = _touristPositionService.Update(touristPosition);
        return CreateResponse(result);
    }

    [HttpGet("{touristId:long}/position")]
    public ActionResult<TouristPositionResponseDto> GetByTouristId(long touristId)
    {
        var result = _touristPositionService.GetByTouristId(touristId);
        return CreateResponse(result);
    }
}
