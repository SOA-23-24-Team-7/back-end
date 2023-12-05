using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class TourSaleDatabaseRepository : ITourSaleRepository
{
    private readonly PaymentsContext _dbContext;
    private readonly DbSet<TourSale> _dbSet;

    public TourSaleDatabaseRepository(PaymentsContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TourSale>();
    }

    public List<TourSale> GetByAuthorId(long authorId)
    {
        var result = _dbSet.ToList().FindAll(s => s.AuthorId == authorId);
        return result;
    }
}
