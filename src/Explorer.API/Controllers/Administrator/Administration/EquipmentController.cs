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
using Grpc.Net.Client;

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
        public async  Task<PagedResult<EquipmentResponse>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetAllEquipment(new EmptyTourMessage{});
            
            var resPaged = new PagedResult<EquipmentResponse>(reply.Equipment.ToList(), reply.Equipment.ToList().Count);

            return resPaged;
        }

        [HttpPost]
        public async Task<EquipmentResponse> Create([FromBody] EquipmentCreateDto equipment)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.CreateEquipment(new EquipmentCreationRequest{ Name = equipment.Name, Description = equipment.Description });

            return reply;
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
