﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<PagedResult<TourRespondeDtoNew>>> GetAuthorsTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = long.Parse(identity.FindFirst("id").Value);

            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, $"tours/authors/{id}");
            // http request to external service
            var response = await _httpClient.GetAsync(uri);

            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<List<TourRespondeDtoNew>>(jsonString);
                foreach(var dto in res)
                {
                    //OVO PROMIJENITI!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    dto.KeyPoints = new List<KeyPointResponseDto>();
                    
                }
                // u paged result
                var resPaged = new PagedResult<TourRespondeDtoNew>(res, res.Count);
                return CreateResponse(FluentResults.Result.Ok(resPaged));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }

            //var result = _tourService.GetAuthorsPagedTours(id, page, pageSize);
            //return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpPost]
        public async Task<ActionResult<TourRespondeDtoNew>> Create([FromBody] TourCreateDto tour)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                tour.AuthorId = long.Parse(identity.FindFirst("id").Value);
            }

            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "tours");
            //preparation for contacting external application
            string requestBody = JsonSerializer.Serialize(tour);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<TourRespondeDtoNew>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
            //var result = _tourService.Create(tour);
            //return CreateResponse(result);
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
        public ActionResult GetEquipment(int tourId)
        {
            var result = _tourService.GetEquipment(tourId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpPost("equipment/{tourId:int}/{equipmentId:int}")]
        public ActionResult AddEquipment(int tourId, int equipmentId)
        {
            var result = _tourService.AddEquipment(tourId, equipmentId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpDelete("equipment/{tourId:int}/{equipmentId:int}")]
        public ActionResult DeleteEquipment(int tourId, int equipmentId)
        {
            var result = _tourService.DeleteEquipment(tourId, equipmentId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("{tourId:long}")]
        public async Task<ActionResult<PagedResult<TourRespondeDtoNew>>> GetById(long tourId)
        {
            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, $"tours/{tourId}");
            var response = await _httpClient.GetAsync(uri);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<TourRespondeDtoNew>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
            //var result = _tourService.GetById(tourId);
            //return CreateResponse(result);
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
