using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IRatingRepository
    {
        Rating? GetByUserId(long id);
        public PagedResult<Rating> GetRatingsPaged(int page, int pageSize);
    }
}
