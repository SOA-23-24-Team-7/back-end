using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface IBundleRecordService
    {
        Result<List<BundleRecordResponseDto>> GetAllByTourist(long touristId);
    }
}
