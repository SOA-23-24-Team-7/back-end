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
        public async Task<ActionResult<PreferenceResponseDto>> Get()
        {
            int id = 0;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                id = int.Parse(identity.FindFirst("id").Value);
            }

            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "tourists/" + id + "/tour-preference");

            var response = await _httpClient.GetAsync(uri);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<PreferenceResponseDto>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<PreferenceResponseDto>> Create([FromBody] PreferenceCreateDto preference)
        {
            preference.UserId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);

            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "tour-preferences");

            string requestBody = JsonSerializer.Serialize(preference);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<PreferenceResponseDto>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
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
