using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubMembershipDatabaseRepository : CrudDatabaseRepository<ClubMembership, StakeholdersContext>, IClubMembershipRepository
{
    public ClubMembershipDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }
}
