using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/wallet")]
    public class WalletController : BaseApiController
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionRecordService _transactionRecordService;
        public WalletController(IWalletService walletService, ITransactionRecordService transactionRecordService)
        {
            _walletService = walletService;
            _transactionRecordService = transactionRecordService;
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
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long adminId;
            if (identity != null && identity.IsAuthenticated)
            {
                adminId = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument));
            }
            var result = _walletService.Update(walletUpdateDto);
            if (!result.IsSuccess || result.IsFailed)
            {
                return NotFound();
            }
            long recieverId = _walletService.Get(walletUpdateDto.Id).Value.TouristId;
            _transactionRecordService.Create(new TransactionRecordCreateDto(recieverId, adminId, walletUpdateDto.AdventureCoin));
            return CreateResponse(result);
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpGet("getTourists")]
        public ActionResult<WalletResponseDto> GetTouristsWalletById([FromQuery] long touristId)
        {
            var result = _walletService.GetForTourist(touristId);
            if (!result.IsSuccess || result.IsFailed)
            {
                return NotFound();
            }
            return CreateResponse(result);
        }
    }
}
