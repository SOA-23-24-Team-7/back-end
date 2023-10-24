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
        PagedResult<ClubJoinRequest> GetPagedByTourist(long id, int page, int pageSize);
        PagedResult<ClubJoinRequest> GetPagedByClub(long id, int page, int pageSize);
        List<ClubJoinRequest> GetAll(Expression<Func<ClubJoinRequest, bool>> filter);
        void DeleteByClubId(long clubId);
        void DeletePending(long clubId, long touristId);
    }
}
