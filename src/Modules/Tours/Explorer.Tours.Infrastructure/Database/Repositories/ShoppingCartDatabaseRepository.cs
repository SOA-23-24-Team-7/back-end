using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ShoppingCartDatabaseRepository : IShoppingCartRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<ShoppingCart> _dbSet;
       public ShoppingCartDatabaseRepository(ToursContext dbContext)
       {
           _dbContext = dbContext;
           _dbSet = _dbContext.Set<ShoppingCart>();
       }
       public ShoppingCart GetByTouristId(long id)
       {
           var entity = _dbSet.Include(x => x.OrderItems).ToList<ShoppingCart>().Find(s => s.TouristId == id && !s.IsPurchased);
           if (entity == null) throw new KeyNotFoundException("Not found: " + id);
           return entity;
       }



          
    }
    
}
