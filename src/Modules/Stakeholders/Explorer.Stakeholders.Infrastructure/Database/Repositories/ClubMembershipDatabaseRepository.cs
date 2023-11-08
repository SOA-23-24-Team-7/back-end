using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class ClubMembershipDatabaseRepository : CrudDatabaseRepository<ClubMembership, StakeholdersContext>, IClubMembershipRepository
{
    public ClubMembershipDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

    public List<ClubMembership> GetAll(Expression<Func<ClubMembership, bool>> filter)
    {
        IQueryable<ClubMembership> query = this.DbContext.ClubMemberships.Include(m => m.Tourist);
        query = query.Where(filter);
        var memberships = query.ToList();
        return memberships;
    }
}
