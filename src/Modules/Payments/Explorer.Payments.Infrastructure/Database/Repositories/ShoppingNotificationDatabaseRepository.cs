using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class ShoppingNotificationDatabaseRepository : IShoppingNotificationRepository
    {
        private readonly PaymentsContext _dbContext;
        private readonly DbSet<ShoppingNotification> _dbSet;
        public ShoppingNotificationDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ShoppingNotification>();
        }
        public PagedResult<ShoppingNotification> GetByTouristId(int page, int pageSize, long id)
        {
            var task = _dbSet.Where(x => x.TouristId == id).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
        public int CountNotSeen(long userId)
        {
            return _dbSet.Count(x => !x.HasSeen && x.TouristId == userId);
        }
    }
}
