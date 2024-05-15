using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Explorer.API.Controllers.Author.TourAuthoring
{
    
    [Route("api/tour")] 
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly IHttpClientService _httpClient;

        public TourController(ITourService tourService, IHttpClientService httpClient)
        {
            _tourService = tourService;
            _httpClient = httpClient;
            
        }

        [Authorize(Roles = "author")]
        [HttpGet]
        public ActionResult<PagedResult<TourResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAllPaged(page, pageSize);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author")]
        [HttpGet("published")]
        public ActionResult<PagedResult<TourResponseDto>> GetPublished([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPublished(page, pageSize);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("authors")]
        public async Task<PagedResult<TourRespondeDtoNew>> GetAuthorsTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var authorId = long.Parse(identity.FindFirst("id").Value);

            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetAllTours(new ToursIdRequest { AuthorId = authorId });

            var tours = reply.Tours.Select(t => {
                var kpReply = client.GetAllKeyPoints(new KeyPointsIdRequest { TourId = t.Id });

                var dto = new TourRespondeDtoNew {
                    Id = t.Id,
                    AuthorId = t.AuthorId,
                    Name = t.Name,
                    Description = t.Description,
                    Difficulty = t.Difficulty,//ok do ovdje
                    Tags = t.Tags.ToList(),
                    Status = (Tours.API.Dtos.TourStatus)t.Status,
                    Price = t.Price,
                    IsDeleted = t.IsDeleted,
                    Distance = t.Distance,
                    AverageRating = t.AverageRating,
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
                    Category = (Tours.API.Dtos.TourCategory)t.Category
                };

                Console.WriteLine("WTFFF");
                Console.WriteLine(dto);

                return dto;
            }).ToList();

            return new PagedResult<TourRespondeDtoNew>(tours, tours.Count());
        }

        [Authorize(Roles = "author, tourist")]
        [HttpPost]
        public async Task<TourResponse> Create([FromBody] TourCreateDto tour)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                tour.AuthorId = long.Parse(identity.FindFirst("id").Value);
            }

            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.CreateTour(new TourCreationRequest{ AuthorId = tour.AuthorId, Category = (int)tour.Category, Description = tour.Description, Difficulty = tour.Difficulty, Distance = (float)tour.Distance, IsDeleted = tour.IsDeleted, Name = tour.Name, Price = (float)tour.Price, Status = (int)tour.Status, Tags = { tour.Tags } });

            return reply;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpPut("{id:int}")]
        public ActionResult<TourResponseDto> Update([FromBody] TourUpdateDto tour)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                tour.AuthorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.DeleteCascade(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("equipment/{tourId:int}")]
        public async Task<PagedResult<EquipmentResponse>> GetEquipment(int tourId)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetTourEquipment(new TourEquipmentListIdRequest { TourId = tourId });

            return new PagedResult<EquipmentResponse>(reply.Equipment.ToList(), reply.Equipment.ToList().Count());
        }

        [Authorize(Roles = "author, tourist")]
        [HttpPost("equipment/{tourId:int}/{equipmentId:int}")]
        public async Task<ActionResult> AddEquipment(int tourId, int equipmentId)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.AddTourEquipment(new TourEquipmentCreationRequest { TourId = tourId, EquipmentId = equipmentId });
            
            return CreateResponse(FluentResults.Result.Ok());
        }

        [Authorize(Roles = "author, tourist")]
        [HttpDelete("equipment/{tourId:int}/{equipmentId:int}")]
        public async Task<ActionResult> DeleteEquipment(int tourId, int equipmentId)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.DeleteTourEquipment(new TourEquipmentDeletionRequest { TourId = tourId, EquipmentId = equipmentId });

            return CreateResponse(FluentResults.Result.Ok());
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("{tourId:long}")]
        public async Task<TourResponse> GetById(long tourId)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetTour(new TourIdRequest { Id = tourId });

            return reply;
        }

        [Authorize(Roles = "author")]
        [HttpPut("publish/{id:int}")]
        public ActionResult<TourResponseDto> Publish(long id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -1;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourService.Publish(id, authorId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author")]
        [HttpPut("archive/{id:int}")]
        public ActionResult<TourResponseDto> Archive(long id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -1;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourService.Archive(id, authorId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "tourist")]
        [HttpPut("markAsReady/{id:int}")]
        public ActionResult<TourResponseDto> MarkAsReady(long id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -1;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _tourService.MarkAsReady(id, authorId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "tourist")]
        [HttpGet("recommended/{publicKeyPointIds}")]
        public ActionResult<TourResponseDto> GetRecommended([FromQuery] int page, [FromQuery] int pageSize, string publicKeyPointIds)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long authorId = -1;
            if (identity != null && identity.IsAuthenticated)
            {
                authorId = long.Parse(identity.FindFirst("id").Value);
            }

            var keyValuePairs = publicKeyPointIds.Split('=');

            var keyPointIdsList = keyValuePairs[1].Split(',').Select(long.Parse).ToList();

            var result = _tourService.GetToursBasedOnSelectedKeyPoints(page, pageSize, keyPointIdsList, authorId);
            return CreateResponse(result);
        }
    }
}
