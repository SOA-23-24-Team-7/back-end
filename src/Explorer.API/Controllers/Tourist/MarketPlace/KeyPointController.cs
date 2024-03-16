using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Tourist.MarketPlace
{
    [Route("api/market-place")]
    public class KeyPointController : BaseApiController
    {
        private readonly IHttpClientService _httpClient;

        public KeyPointController(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/{tourId:long}/key-points")]
        public async Task<ActionResult<KeyPointResponseDto[]>> GetKeyPoints(long tourId)
        {
            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "tours/" + tourId + "/key-points");

            var response = await _httpClient.GetAsync(uri);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<KeyPointResponseDto[]>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
        }

        [Authorize(Roles = "tourist")]
        [HttpGet("{campaignId:long}/key-points")]
        public ActionResult<KeyPointResponseDto> GetCampaignKeyPoints(long campaignId)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "author, tourist")]
        [HttpGet("tours/{tourId:long}/firts-key-point")]
        public ActionResult<KeyPointResponseDto> GetToursFirstKeyPoint(long tourId)
        {
            throw new NotImplementedException();
        }
    }
}
