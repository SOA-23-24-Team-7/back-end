using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubRepository : ICrudRepository<Club>
    {
        public PagedResult<Club> GetClubsPaged(int page, int pageSize);
    }
}
