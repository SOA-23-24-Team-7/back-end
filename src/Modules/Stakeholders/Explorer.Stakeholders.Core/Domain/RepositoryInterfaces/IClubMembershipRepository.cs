using Explorer.BuildingBlocks.Core.UseCases;
using System.Linq.Expressions;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IClubMembershipRepository : ICrudRepository<ClubMembership>
{
    List<ClubMembership> GetAll(Expression<Func<ClubMembership, bool>> filter);
}
