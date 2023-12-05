using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories;

public class TourSaleDatabaseRepository : CrudDatabaseRepository<TourSale, PaymentsContext>, ITourSaleRepository
{
    public TourSaleDatabaseRepository(PaymentsContext dbContext): base(dbContext) { }

    public List<TourSale> GetByAuthorId(long authorId)
    {
        var result = DbContext.TourSales.ToList().FindAll(s => s.AuthorId == authorId);
        return result;
    }

    public TourSale Update(TourSale oldSale, TourSale newSale)
    {
        DbContext.Entry(oldSale).CurrentValues.SetValues(newSale);

        DbContext.SaveChanges();

        return newSale;
    }
}
