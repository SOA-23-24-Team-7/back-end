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
    public class ClubJoinRequestRepository : IClubJoinRequestRepository
    {
        private readonly StakeholdersContext _dbContext;

        public ClubJoinRequestRepository(StakeholdersContext dbContext)
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

        public ClubJoinRequest GetAsNoTracking(Expression<Func<ClubJoinRequest, bool>> filter)
        {
            IQueryable<ClubJoinRequest> query = _dbContext.ClubJoinRequests;
            query = query.Where(filter);
            var request = query.AsNoTracking().FirstOrDefault();
            if (request == null) throw new KeyNotFoundException("Club Join Request Not found");
            return request;
        }

        public ClubJoinRequest Create(ClubJoinRequest request)
        {
            _dbContext.ClubJoinRequests.Add(request);
            _dbContext.SaveChanges();
            return request;
        }

        public ClubJoinRequest Update(ClubJoinRequest request)
        {
            _dbContext.ClubJoinRequests.Update(request);
            _dbContext.SaveChanges();
            return request;
        }
    }
}
