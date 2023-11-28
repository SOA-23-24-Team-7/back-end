using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/wallet")]
    public class WalletController : BaseApiController
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Authorize(Policy = "touristPolicy")]
        [HttpGet]
        public ActionResult<WalletResponseDto> GetTouristsWallet()
        {
            int touristId = int.Parse(HttpContext.User.Claims.First(i => i.Type.Equals("id", StringComparison.OrdinalIgnoreCase)).Value);
            var result = _walletService.GetForTourist(touristId);
            if(!result.IsSuccess || result.IsFailed)
            {
                return NotFound();
            }
            return CreateResponse(result);
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpPut("{id:long}")]
        public ActionResult<WalletResponseDto> UpdateWallet([FromBody] WalletUpdateDto walletUpdateDto)
        {
            var result = _walletService.Update(walletUpdateDto);
            if (!result.IsSuccess || result.IsFailed)
            {
                return NotFound();
            }
            return CreateResponse(result);
        }
    }
}
