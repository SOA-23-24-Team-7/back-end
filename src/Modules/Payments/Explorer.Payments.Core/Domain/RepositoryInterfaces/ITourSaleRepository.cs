namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface ITourSaleRepository
{
    List<TourSale> GetByAuthorId(long authorId);
}
