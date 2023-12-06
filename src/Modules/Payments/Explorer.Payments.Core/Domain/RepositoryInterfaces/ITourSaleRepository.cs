using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System.Linq.Expressions;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ITourSaleRepository
{
    List<TourSale> GetByAuthorId(long authorId);
    TourSale Get(long id);
    List<TourSale> GetAll();
    TourSale Create(TourSale sale);
    TourSale Update(TourSale oldSale, TourSale newSale);
    void Delete(long id);
}
