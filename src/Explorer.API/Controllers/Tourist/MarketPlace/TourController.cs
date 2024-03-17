using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;

namespace Explorer.API.Controllers.Tourist.MarketPlace
{
    [Route("api/market-place")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IHttpClientService _httpClient;

        public TourController(ITourService service, IShoppingCartService shoppingCartService, IHttpClientService httpClient)
        {
            _tourService = service;
            _shoppingCartService = shoppingCartService;
            _httpClient = httpClient;
            
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/published")]
        public ActionResult<PagedResult<LimitedTourViewResponseDto>> GetPublishedTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPublishedLimitedView(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("tours/{tourId:long}")]
        public async Task<ActionResult<PagedResult<TourResponseDto>>> GetById(long tourId)
        {
            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, $"tours/{tourId}");
            var response = await _httpClient.GetAsync(uri);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<TourRespondeDtoNew>(jsonString);

                string keyPointUri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "tours/" + res.Id + "/key-points");

                var keyPointResponse = await _httpClient.GetAsync(keyPointUri);
                if (keyPointResponse != null && keyPointResponse.IsSuccessStatusCode)
                {
                    var keyPointJsonString = await keyPointResponse.Content.ReadAsStringAsync();
                    var keyPointRes = JsonSerializer.Deserialize<KeyPointResponseDto[]>(keyPointJsonString);

                    res.KeyPoints = new List<KeyPointResponseDto>(keyPointRes);
                }
                else
                {
                    return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
                }

                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
            //var result = _tourService.GetById(tourId);
            //return CreateResponse(result);
        }

        [HttpGet("tours/can-be-rated/{tourId:long}")]
        public bool CanTourBeRated(long tourId)
        {
            long userId = extractUserIdFromHttpContext();
            return _tourService.CanTourBeRated(tourId, userId).Value;
        }

        private long extractUserIdFromHttpContext()
        {
            return long.Parse((HttpContext.User.Identity as ClaimsIdentity).FindFirst("id")?.Value);
        }

        [Authorize(Policy = "touristPolicy")]
        [Authorize(Roles = "tourist")]
        [HttpGet("tours/inCart/{id:long}")]
        public ActionResult<PagedResult<LimitedTourViewResponseDto>> GetToursInCart([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            var cart = _shoppingCartService.GetByTouristId(id);
            if (cart.Value == null)
            {
                return NotFound();
            }
            var tourIds = cart.Value.OrderItems.Select(order => order.TourId).ToList();
            var result = _tourService.GetLimitedInfoTours(page, pageSize, tourIds);
            return CreateResponse(result);
        }
        /*[HttpGet("tours/inCart/{id:long}")]
        public ActionResult<PagedResult<LimitedTourViewResponseDto>> GetToursInCart([FromQuery] int page, [FromQuery] int pageSize, long id)
        {
            var cart = _shoppingCartService.GetByTouristId(id);
            if (cart == null)
            {
                return NotFound();
            }
            var tourIds = cart.Value.OrderItems.Select(order => order.TourId).ToList();
            var result = _tourService.GetLimitedInfoTours(page, pageSize, tourIds);
            return CreateResponse(result);
        }*/

        [HttpGet("tours/adventure")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularAdventureTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAdventureTours(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("tours/family")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularFamilyTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetFamilyTours(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("tours/cruise")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularCruiseTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetCruiseTours(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("tours/cultural")]
        public ActionResult<PagedResult<TourResponseDto>> GetPopularCulturalTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetCulturalTours(page, pageSize);
            return CreateResponse(result);
        }

    }
}
