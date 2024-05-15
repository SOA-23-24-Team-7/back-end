using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Grpc.Net.Client;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/facility")]
    public class FacilityController : BaseApiController
    {
        private readonly IHttpClientService _httpClient;

        public FacilityController(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public ActionResult<PagedResult<FacilityResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            throw new NotImplementedException();
        }

        [HttpGet("authorsFacilities")]
        public async Task<PagedResult<FacilityResponse>> GetByAuthorId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInAuthorId = int.Parse(identity.FindFirst("id").Value);

            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetAllFacilities(new FacilitiesIdRequest{ AuthorId = loggedInAuthorId });

            var resPaged = new PagedResult<FacilityResponse>(reply.Facilities.ToList(), reply.Facilities.ToList().Count);

            return resPaged;
        }

        [HttpPost]
        public async Task<FacilityResponse> Create([FromBody] FacilityCreateDto facility)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                facility.AuthorId = int.Parse(identity.FindFirst("id").Value);
            }

            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.CreateFacility(new FacilityCreationRequest{ AuthorId = facility.AuthorId, Category = (long)facility.Category, Description = facility.Description, ImagePath = facility.ImagePath, Latitude = (float)facility.Latitude, Longitude = (float)facility.Longitude, Name = facility.Name });

            return reply;
        }

        [HttpPut("{id:int}")]
        public ActionResult<FacilityResponseDto> Update([FromBody] FacilityUpdateDto facility)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
