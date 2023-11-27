using Explorer.Payments.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/wallet")]
    public class WalletController : BaseApiController
    {
        public WalletController() { }

        [Authorize(Policy = "touristPolicy")]
        [HttpGet]
        public ActionResult<WalletResponseDto> GetTouristsWallet()
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpPut("{id:long}")]
        public ActionResult<WalletResponseDto> UpdateWallet([FromBody] WalletUpdateDto walletUpdateDto)
        { 
            throw new NotImplementedException();
        }
    }
}
