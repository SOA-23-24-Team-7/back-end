using Explorer.Tours.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;
using Grpc.Net.Client;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/preferences")]
    public class PreferenceController : BaseApiController
    {
        private readonly IHttpClientService _httpClient;

        public PreferenceController(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<TourPreferenceResponse> Get()
        {
            int id = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                id = int.Parse(identity.FindFirst("id").Value);
            }

            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetPreference(new TourPreferenceIdRequest { UserId = id });

            return reply;
        }

        [HttpPost("create")]
        public async Task<TourPreferenceResponse> Create([FromBody] PreferenceCreateDto preference)
        {
            preference.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.CreatePreference(new TourPreferenceCreationRequest { UserId = preference.UserId, BoatRating = preference.BoatRating, CarRating = preference.CarRating, CyclingRating = preference.CyclingRating, DifficultyLevel = preference.DifficultyLevel, SelectedTags = {preference.SelectedTags }, WalkingRating = preference.WalkingRating });

            return reply;
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public ActionResult<PreferenceResponseDto> Update([FromBody] PreferenceUpdateDto preference)
        {
            throw new NotImplementedException();
        }
    }
}
