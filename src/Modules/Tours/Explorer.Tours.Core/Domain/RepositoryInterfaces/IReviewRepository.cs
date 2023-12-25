using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IReviewRepository
    {
        PagedResult<Review> GetPagedByTourId(int page, int pageSize, long tourId);
        bool ReviewExists(long touristId, long tourId);
        int GetTourReviewCounts(long tourId, int forLastNDays);
        double? GetTourReviewAverageRating(long tourId, int forLastNDays);

        int GetTourReviewCountsAllTime(long tourId);
        double? GetTourReviewAverageRatingAllTime(long tourId);
    }
}
