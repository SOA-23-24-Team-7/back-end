using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/equipment")]
    public class EquipmentController : BaseApiController
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IHttpClientService _httpClientService;

        public EquipmentController(IEquipmentService equipmentService, IHttpClientService httpClientService)
        {
            _equipmentService = equipmentService;
            this._httpClientService = httpClientService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EquipmentResponseDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8087, "equipment");
            // http request to external service
            var response = await _httpClientService.GetAsync(uri);

            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<List<EquipmentResponseDto>>(jsonString);
                // u paged result
                var resPaged = new PagedResult<EquipmentResponseDto>(res, res.Count);
                return CreateResponse(FluentResults.Result.Ok(resPaged));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
            //var result = _equipmentService.GetPaged(page, pageSize);
            //return CreateResponse(result);
        }
    }
}
