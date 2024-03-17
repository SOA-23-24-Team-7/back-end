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
    public async Task<ActionResult<KeyPointResponseDto>> CreateKeyPoint([FromRoute] long tourId, [FromBody] KeyPointCreateDto keyPoint)
    {
        keyPoint.TourId = tourId;

        string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "key-points");

        string requestBody = JsonSerializer.Serialize(keyPoint);
        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);
        if (response != null && response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<KeyPointResponseDto>(jsonString);
            return CreateResponse(FluentResults.Result.Ok(res));
        }
        else
        {
            return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
        }
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
