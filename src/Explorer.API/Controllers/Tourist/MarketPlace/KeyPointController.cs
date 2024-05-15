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
using Grpc.Net.Client;

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
        public async Task<KeyPointResponse[]> GetKeyPoints(long tourId)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetAllKeyPoints(new KeyPointsIdRequest{ TourId = tourId });

            return reply.KeyPoints.ToArray();
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
