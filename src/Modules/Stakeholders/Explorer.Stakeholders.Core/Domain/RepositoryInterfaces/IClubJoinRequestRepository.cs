using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubJoinRequestRepository
    {
        ClubJoinRequest Get(Expression<Func<ClubJoinRequest, bool>> filter);
        ClubJoinRequest GetAsNoTracking(Expression<Func<ClubJoinRequest, bool>> filter);
        ClubJoinRequest Create(ClubJoinRequest request);
        ClubJoinRequest Update(ClubJoinRequest request);
    }
}
