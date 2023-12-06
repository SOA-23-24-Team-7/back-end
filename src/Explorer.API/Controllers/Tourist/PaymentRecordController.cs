using System.Security.Claims;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/record")]
    public class PaymentRecordController : BaseApiController
    {
        private readonly IRecordService _recordService;
        private readonly ITransactionRecordService _transactionRecordService;
        public PaymentRecordController(IRecordService recordService, ITransactionRecordService transactionRecordService)
        {
            _recordService = recordService;
            _transactionRecordService = transactionRecordService;
        }

        [HttpGet]
        public ActionResult<PagedResult<RecordResponseDto>> GetPagedByTouristId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long id;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument));
            }
            var result = _recordService.GetPagedByTouristId(page, pageSize, id);
            return CreateResponse(result);
        }


        [HttpGet("transactions")]
        public ActionResult<PagedResult<TransactionRecordResponseDto>> GetPagedTransactionsByTouristId([FromQuery] int page, [FromQuery] int pageSize)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            long id;
            if (identity != null && identity.IsAuthenticated)
            {
                id = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return CreateResponse(Result.Fail(FailureCode.InvalidArgument));
            }
            var result = _transactionRecordService.GetPagedTransactionsByTouristId(page, pageSize, id);
            return CreateResponse(result);
        }
    }
}
