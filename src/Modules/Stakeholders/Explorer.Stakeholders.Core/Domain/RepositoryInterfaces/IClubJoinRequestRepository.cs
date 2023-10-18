using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubJoinRequestRepository: ICrudRepository<ClubJoinRequest>
    {
        ClubJoinRequest Get(Expression<Func<ClubJoinRequest, bool>> filter);
        ClubJoinRequest Create(ClubJoinRequest request);
        ClubJoinRequest Update(ClubJoinRequest request);
    }
}
