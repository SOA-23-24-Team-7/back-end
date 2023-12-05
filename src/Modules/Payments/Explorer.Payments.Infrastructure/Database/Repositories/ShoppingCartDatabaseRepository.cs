using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class ShoppingCartDatabaseRepository : IShoppingCartRepository
    {
        private readonly PaymentsContext _dbContext;
        private readonly DbSet<ShoppingCart> _dbSet;
        public ShoppingCartDatabaseRepository(PaymentsContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ShoppingCart>();
        }

        public ShoppingCart Get(long id)
        {
            return _dbSet.Include(x => x.OrderItems).Where(x => x.Id == id).FirstOrDefault();
        }

        public ShoppingCart GetByTouristId(long id)
        {
            var entity = _dbSet.Include(sc => sc.OrderItems).Include(sc => sc.BundleOrderItems).ToList().Find(s => s.TouristId == id && !s.IsPurchased);
            //if (entity == null) throw new KeyNotFoundException("Not found: " + id);
            return entity;
        }
    }
}
