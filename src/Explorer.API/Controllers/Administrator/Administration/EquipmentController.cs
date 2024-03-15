using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/equipment")]
    public class EquipmentController : BaseApiController
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IHttpClientService _httpClientService;

        public EquipmentController(IEquipmentService equipmentService, IHttpClientService httpClientService)
        {
            _equipmentService = equipmentService;
            _httpClientService = httpClientService;
        }

        [HttpGet]
        public async  Task<ActionResult<PagedResult<EquipmentResponseDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
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

        [HttpPost]
        public async Task<ActionResult<EquipmentResponseDto>> Create([FromBody] EquipmentCreateDto equipment)
        {

            string uri = _httpClientService.BuildUri(Protocol.HTTP, "localhost", 8087, "equipment");
            //preparation for contacting external application
            string requestBody = JsonSerializer.Serialize(equipment);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var response = await _httpClientService.PostAsync(uri, content);
            if (response != null && response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<EquipmentResponseDto>(jsonString);
                return CreateResponse(FluentResults.Result.Ok(res));
            }
            else
            {
                return CreateResponse(FluentResults.Result.Fail(FailureCode.InvalidArgument));
            }
            //var result = _equipmentService.Create(equipment);
            //return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<EquipmentResponseDto> Update([FromBody] EquipmentUpdateDto equipment)
        {
            var result = _equipmentService.Update(equipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _equipmentService.Delete(id);
            return CreateResponse(result);
        }
    }
}
