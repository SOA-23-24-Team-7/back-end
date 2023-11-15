using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubInvitationDatabaseRepository : CrudDatabaseRepository<ClubInvitation, StakeholdersContext>, IClubInvitationRepository
{
    public ClubInvitationDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

    public void DeleteByClubId(long clubId)
    {
        var invitations = GetAll(i => i.ClubId == clubId);
        foreach (var invitation in invitations)
        {
            Delete(invitation.Id);
        }
    }

    public void DeleteWaiting(long clubId, long touristId)
    {
        var invitations = GetAll(i => i.ClubId == clubId && i.TouristId == touristId && i.Status == InvitationStatus.Waiting);
        foreach (var invitation in invitations)
        {
            Delete(invitation.Id);
        }
    }

    public List<ClubInvitation> GetAll(Expression<Func<ClubInvitation, bool>> filter)
    {
        IQueryable<ClubInvitation> query = this.DbContext.ClubInvitations.Include(i => i.Club).Include(i => i.Club.Owner);
        query = query.Where(filter);
        var invitations = query.ToList();
        return invitations;
    }
}
