using Explorer.Payments.API.Dtos;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/tour-sales")]
public class TourSaleController : BaseApiController
{
    [HttpPost]
    public ActionResult<TourSaleResponseDto> Create([FromBody] TourSaleCreateDto request)
    {
        throw new NotImplementedException();
    }
}
