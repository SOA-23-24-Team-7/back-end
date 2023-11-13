using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.Core.UseCases;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/shoppingCart")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id:long}")]
        public ActionResult<ShoppingCartResponseDto> GetByTouristId(long id)
        {
            var result = _cartService.GetByTouristId(id);
            return CreateResponse(result);
        }



        [HttpPost]
        public ActionResult<ShoppingCartResponseDto> Create([FromBody] ShoppingCartCreateDto cart)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                cart.TouristId = long.Parse(identity.FindFirst("id").Value);
            }
            var result = _cartService.Create(cart);
            return CreateResponse(result);
        }

        [HttpPut("{id:long}")]
        public ActionResult<ShoppingCartResponseDto> Update([FromBody] ShoppingCartUpdateDto cart)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                if (long.Parse(identity.FindFirst("id").Value) != cart.TouristId)
                    return Forbid();
            }
            var result = _cartService.Update(cart);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _cartService.Delete(id);
            return CreateResponse(result);
        }


        [HttpPost("addItem")]
        public ActionResult AddOrderItem([FromBody] OrderItemCreateDto item)
        {
            var result = _cartService.AddOrderItem(item);
            return CreateResponse(result);

        }

        [HttpDelete("removeItem/{id:int}/{shoppingCartId:int}")]
        public ActionResult RemoveOrderItem(int id, int shoppingCartId)
        {
            var result = _cartService.RemoveOrderItem(id, shoppingCartId);
            return CreateResponse(result);

        }
        [HttpGet("getItem/{tourId:long}/{touristId:long}")]
        public ActionResult<OrderItemResponseDto> GetItemByTourId(long tourId,long touristId)
        {
            var result = _cartService.GetItemByTourId(tourId,touristId);
            return CreateResponse(result);

        }
 
    }
}
