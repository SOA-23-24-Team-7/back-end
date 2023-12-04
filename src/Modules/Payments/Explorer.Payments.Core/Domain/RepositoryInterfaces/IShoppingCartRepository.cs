using Explorer.Payments.Core.Domain.ShoppingCarts;


namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetByTouristId(long id);
        ShoppingCart Get(long id);
    }
}
