using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ITransactionRecordRepository
    {
        PagedResult<TransactionRecord> GetPagedTransactionsByTourist(int page, int pageSize, long touristId);
    }
}
