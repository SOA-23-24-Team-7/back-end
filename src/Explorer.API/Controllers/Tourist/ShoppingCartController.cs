using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

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
    }
}
