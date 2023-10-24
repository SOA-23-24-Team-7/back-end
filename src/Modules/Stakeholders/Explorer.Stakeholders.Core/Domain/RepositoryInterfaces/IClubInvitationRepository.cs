using Explorer.BuildingBlocks.Core.UseCases;
using System.Linq.Expressions;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IClubInvitationRepository : ICrudRepository<ClubInvitation>
{
    List<ClubInvitation> GetAll(Expression<Func<ClubInvitation, bool>> filter);
    void DeleteByClubId(long clubId);
    void DeleteWaiting(long clubId, long touristId);
}
