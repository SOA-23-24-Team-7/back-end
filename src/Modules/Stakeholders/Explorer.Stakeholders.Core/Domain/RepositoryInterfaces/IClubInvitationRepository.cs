using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IClubInvitationRepository : ICrudRepository<ClubInvitation>
{
    bool Exists(ClubInvitation clubInvitation);
}
