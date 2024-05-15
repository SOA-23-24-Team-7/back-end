using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Payments.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.NetworkInformation;
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
        public async Task<TourResponseDto> GetById(long tourId)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetTour(new TourIdRequest{ Id = tourId });
            var kpReply = client.GetAllKeyPoints(new KeyPointsIdRequest { TourId = tourId });

            return new TourResponseDto {
                Id = reply.Id,
                AuthorId = reply.AuthorId,
                Name = reply.Name,
                Description = reply.Description,
                Difficulty = reply.Difficulty,
                Tags = reply.Tags.ToList(),
                Status = (Tours.API.Dtos.TourStatus)reply.Status,
                Price = reply.Price,
                IsDeleted = reply.IsDeleted,
                Distance = reply.Distance,
                AverageRating = reply.AverageRating,
                KeyPoints = kpReply.KeyPoints.Select(kp => new KeyPointResponseDto {
                    Id = kp.Id,
                    TourId = kp.TourId,
                    Name = kp.Name,
                    Description = kp.Description,
                    Longitude = kp.Longitude,
                    Latitude = kp.Latitude,
                    LocationAddress = kp.LocationAddress,
                    ImagePath = kp.ImagePath,
                    Order = kp.Order,
                    HaveSecret = kp.HaveSecret,
                    Secret = kp.HaveSecret == true ? new KeyPointSecretDto { Images = kp.Secret.Images.ToList(), Description = kp.Secret.Description } : null
                }).ToList(),
                Category = (Tours.API.Dtos.TourCategory)reply.Category
            };
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
