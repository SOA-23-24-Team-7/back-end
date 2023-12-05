using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.ShoppingCarts;

public class OrderItem : Entity
{
    public long TourId { get; init; }
    public string TourName { get; init; }
    public double Price { get; private set; }
    public long ShoppingCartId { get; init; }

    public OrderItem(long tourId, string tourName, double price, long shoppingCartId) 
    {
        TourId = tourId;
        TourName = tourName;
        Price = price;
        ShoppingCartId = shoppingCartId;
    }

    public void ApplyDiscount(double discount)
    {
        Price = Price - Price * discount / 100.0;
    }
}
