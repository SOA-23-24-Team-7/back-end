using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.HTTP.Interfaces;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Grpc.Net.Client;
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
        public async Task<ActionResult<PagedResult<EquipmentResponse>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            using var channel = GrpcChannel.ForAddress("http://tour-service:8087");
            var client = new TourMicroservice.TourMicroserviceClient(channel);
            var reply = client.GetAllEquipment(new EmptyTourMessage{});

            var resPaged = new PagedResult<EquipmentResponse>(reply.Equipment.ToList(), reply.Equipment.ToList().Count);

            return resPaged;
        }
    }
}
