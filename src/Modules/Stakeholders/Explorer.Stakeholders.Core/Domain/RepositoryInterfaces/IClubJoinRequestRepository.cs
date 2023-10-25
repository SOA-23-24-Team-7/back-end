using Explorer.BuildingBlocks.Core.UseCases;
using System.Linq.Expressions;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubJoinRequestRepository: ICrudRepository<ClubJoinRequest>
    {
        ClubJoinRequest Get(Expression<Func<ClubJoinRequest, bool>> filter);
        PagedResult<ClubJoinRequest> GetPagedByTourist(long id, int page, int pageSize);
        PagedResult<ClubJoinRequest> GetPagedByClub(long id, int page, int pageSize);
        List<ClubJoinRequest> GetAll(Expression<Func<ClubJoinRequest, bool>> filter);
        void DeleteByClubId(long clubId);
        void DeletePending(long clubId, long touristId);
    }
}
