using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface ITransactionRecordService
    {
        Result<TransactionRecordResponseDto> Create<TransactionRecordCreateDto>(TransactionRecordCreateDto transaction);
        Result<PagedResult<TransactionRecordResponseDto>> GetPagedTransactionsByTouristId(int page, int pageSize, long touristId);
    }
}
