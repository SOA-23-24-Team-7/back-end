using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class CouponDatabaseRepository : ICouponRepository
    {
        private readonly PaymentsContext _dbContext;
        private readonly DbSet<Coupon> _dbSet;
        public CouponDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Coupon>();
        }

        public Coupon FindByCode(string code)
        {
           return _dbSet.Where(x => x.Code == code).FirstOrDefault();
            
        }

        public PagedResult<Coupon> GetPagedByAuthorId(int page, int pageSize, long id)
        {
            var task = _dbSet.Where(x => x.AuthorId == id).GetPagedById(page, pageSize);
            //var task = _dbContext.Reviews.Include(r => r.Tour).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
