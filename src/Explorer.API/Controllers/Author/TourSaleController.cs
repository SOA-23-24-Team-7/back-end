using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author;

[Authorize(Policy = "authorPolicy")]
[Route("api/tour-sales")]
public class TourSaleController : BaseApiController
{
    private readonly ITourSaleService _saleService;

    public TourSaleController(ITourSaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpPost]
    public ActionResult<TourSaleResponseDto> Create([FromBody] TourSaleCreateDto request)
    {
        var result = _saleService.Create(request);
        return CreateResponse(result);
    }
}
