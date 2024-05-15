using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.TourAuthoring;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.TourAuthoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Grpc.Net.Client;

namespace Explorer.API.Controllers.Author.TourAuthoring;

[Route("api/tour-authoring")]
public class KeyPointController : BaseApiController
{
    private readonly IHttpClientService _httpClient;

    public KeyPointController(IHttpClientService httpClient)
    {
        _httpClient = httpClient;
    }

    [Authorize(Roles = "author")]
    [HttpPost("tours/{tourId:long}/key-points")]
    public async Task<KeyPointResponse> CreateKeyPoint([FromRoute] long tourId, [FromBody] KeyPointCreateDto keyPoint)
    {
        keyPoint.TourId = tourId;

        using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
        var client = new TourMicroservice.TourMicroserviceClient(channel);
        var secret = keyPoint.Secret != null ? new KeyPointSecretCreationRequest{ Images = { keyPoint.Secret.Images }, Description = keyPoint.Secret.Description } : null;
        var reply = client.CreateKeyPoint(new KeyPointCreationRequest{ Name = keyPoint.Name, Description = keyPoint.Description, ImagePath = keyPoint.ImagePath, Latitude = (float)keyPoint.Latitude, Longitude = (float)keyPoint.Longitude, LocationAddress = keyPoint.LocationAddress, Order = (int)keyPoint.Order, Secret = secret, TourId = keyPoint.TourId });

        return reply;
    }

    [Authorize(Roles = "author")]
    [HttpPut("tours/{tourId:long}/key-points/{id:long}")]
    public ActionResult<KeyPointResponseDto> Update(long tourId, long id, [FromBody] KeyPointUpdateDto keyPoint)
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = "author, tourist")]
    [HttpDelete("tours/{tourId:long}/key-points/{id:long}")]
    public ActionResult Delete(long tourId, long id)
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = "author")]
    [HttpGet]
    public ActionResult<PagedResult<KeyPointResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
    {
        throw new NotImplementedException();
    }
}
