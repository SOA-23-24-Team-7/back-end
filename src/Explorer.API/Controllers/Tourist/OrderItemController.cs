using Explorer.Payments.API.Dtos; //MENJANO
using Explorer.Payments.API.Public; //MENJANO
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/orderItem")]
    public class OrderItemController:BaseApiController
    {
        private readonly IOrderItemService _orderService;

        public OrderItemController(IOrderItemService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<PagedResult<OrderItemResponseDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _orderService.GetPaged(page,pageSize);
            return CreateResponse(result);
        }



        [HttpPost]
        public ActionResult<OrderItemResponseDto> Create([FromBody] OrderItemCreateDto item)
        {
            var result = _orderService.Create(item);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<OrderItemResponseDto> Update([FromBody] OrderItemUpdateDto item)
        {
            var result = _orderService.Update(item);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _orderService.Delete(id);
            return CreateResponse(result);
        }
    }
}
