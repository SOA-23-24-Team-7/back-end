using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubJoinRequestRepository : CrudDatabaseRepository<ClubJoinRequest, StakeholdersContext>, IClubJoinRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubJoinRequestRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public ClubJoinRequest Get(Expression<Func<ClubJoinRequest, bool>> filter)
        {
            IQueryable<ClubJoinRequest> query = _dbContext.ClubJoinRequests;
            query = query.Where(filter);
            var request = query.FirstOrDefault();
            if (request == null) throw new KeyNotFoundException("Club Join Request Not found");
            return request;
        }

        public PagedResult<ClubJoinRequest> GetPagedByTourist(long id, int page, int pageSize)
        {
            Expression<Func<ClubJoinRequest, bool>> filter = (r => r.TouristId == id && r.Status != ClubJoinRequestStatus.Cancelled);
            var getTask = _dbContext.ClubJoinRequests.Include(r => r.Club).Where(filter).GetPaged(page, pageSize);
            getTask.Wait();
            return getTask.Result;
        }

        public PagedResult<ClubJoinRequest> GetPagedByClub(long id, int page, int pageSize)
        {
            Expression<Func<ClubJoinRequest, bool>> filter = (r => r.ClubId == id && r.Status != ClubJoinRequestStatus.Cancelled);
            var getTask = _dbContext.ClubJoinRequests.Include(r => r.Tourist).Where(filter).GetPaged(page, pageSize);
            getTask.Wait();
            return getTask.Result;
        }

        public List<ClubJoinRequest> GetAll(Expression<Func<ClubJoinRequest, bool>> filter)
        {
            IQueryable<ClubJoinRequest> query = _dbContext.ClubJoinRequests;
            query = query.Where(filter);
            var requests = query.ToList();
            return requests;
        }

        public void DeleteByClubId(long clubId)
        {
            var requests = GetAll(r => r.ClubId == clubId);
            foreach (var request in requests)
            {
                Delete(request.Id);
            }
        }

        public void DeletePending(long clubId, long touristId)
        {
            var requests = GetAll(r => r.ClubId == clubId && r.TouristId == touristId && r.Status == ClubJoinRequestStatus.Pending);
            foreach (var request in requests)
            {
                Delete(request.Id);
            }
        }
    }
}
