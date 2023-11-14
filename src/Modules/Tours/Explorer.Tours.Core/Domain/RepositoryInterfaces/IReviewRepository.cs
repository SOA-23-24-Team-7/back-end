using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IReviewRepository
    {
        PagedResult<Review> GetPagedByTourId(int page, int pageSize, long tourId);
        bool ReviewExists(long touristId, long tourId);
    }
}
