using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubInvitationDatabaseRepository : CrudDatabaseRepository<ClubInvitation, StakeholdersContext>, IClubInvitationRepository
{
    public ClubInvitationDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

    public List<ClubInvitation> GetAll(Expression<Func<ClubInvitation, bool>> filter)
    {
        IQueryable<ClubInvitation> query = this.DbContext.ClubInvitations;
        query = query.Where(filter);
        var invitations = query.ToList();
        return invitations;
    }
}
