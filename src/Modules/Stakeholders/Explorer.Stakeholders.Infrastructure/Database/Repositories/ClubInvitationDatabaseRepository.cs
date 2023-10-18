using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubInvitationDatabaseRepository : CrudDatabaseRepository<ClubInvitation, StakeholdersContext>, IClubInvitationRepository
{
    public ClubInvitationDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

    public bool Exists(ClubInvitation clubInvitation)
    {
        return DbContext.ClubInvitations.Any(invitation => invitation == clubInvitation);
    }
}
