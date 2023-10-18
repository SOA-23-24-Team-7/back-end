using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            var task = _dbContext.ClubJoinRequests.Include(r => r.Club).Where(r => r.TouristId == id && r.Status != ClubJoinRequestStatus.Cancelled).GetPaged(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
