using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubInvitationDatabaseRepository : CrudDatabaseRepository<ClubInvitation, StakeholdersContext>, IClubInvitationRepository
{
    public ClubInvitationDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

    public Collection<ClubInvitation> GetWaiting()
    {
        throw new NotImplementedException();
    }
}
