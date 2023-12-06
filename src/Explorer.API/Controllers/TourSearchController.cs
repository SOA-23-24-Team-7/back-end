using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Authorize(Policy = "nonAdministratorPolicy")]
    [Route("api/tourist/tour")]
    public class TourSearchController : BaseApiController
    {
        private readonly ITourSearchService _tourSearchService;
        private readonly ITourSaleService? _tourSaleService;

        public TourSearchController(ITourSearchService tourSearchService, ITourSaleService? tourSaleService)
        {
            _tourSearchService = tourSearchService;
            _tourSaleService = tourSaleService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourResponseDto>> SearchByGeoLocation([FromQuery] double longitude, [FromQuery] double latitude, [FromQuery] double maxDistance, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourSearchService.SearchByLocation(longitude, latitude, maxDistance, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("search")]
        public ActionResult<PagedResult<TourResponseDto>> Search([FromQuery] TourSearchFilterDto tourSearchFilterDto, [FromQuery] SortOption? sortBy = null)
        {
            Func<long, double?>? getDiscount = null;

            if (_tourSaleService != null)
            {
                getDiscount = tourId => _tourSaleService.GetDiscountForTour(tourId).Value;
            }

            var result = _tourSearchService.Search(tourSearchFilterDto, sortBy == null ? SortOption.NoSort : (SortOption)sortBy, true, getDiscount);
            return CreateResponse(result);
        }

        [HttpGet("author-search")]
        public ActionResult<PagedResult<TourResponseDto>> AuthorSearch([FromQuery] TourSearchFilterDto tourSearchFilterDto, [FromQuery] SortOption? sortBy = null)
        {
            Func<long, double?>? getDiscount = null;

            if (_tourSaleService != null)
            {
                getDiscount = tourId => _tourSaleService.GetDiscountForTour(tourId).Value;
            }

            var result = _tourSearchService.Search(tourSearchFilterDto, sortBy == null ? SortOption.NoSort : (SortOption)sortBy, false, getDiscount);
            return CreateResponse(result);
        }
    }
}
