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
        public async Task<ActionResult<PagedResult<FacilityResponseDto>>> GetByAuthorId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var loggedInAuthorId = int.Parse(identity.FindFirst("id").Value);

            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "authors/" + loggedInAuthorId + "/facilities");

            var response = await _httpClient.GetAsync(uri);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<FacilityResponseDto[]>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(new PagedResult<FacilityResponseDto>(res.ToList(), res.Length)));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
        }

        [HttpPost]
        public async Task<ActionResult<FacilityResponseDto>> Create([FromBody] FacilityCreateDto facility)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                facility.AuthorId = int.Parse(identity.FindFirst("id").Value);
            }

            string uri = _httpClient.BuildUri(Protocol.HTTP, "localhost", 8087, "facilities");

            string requestBody = JsonSerializer.Serialize(facility);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<FacilityResponseDto>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
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
