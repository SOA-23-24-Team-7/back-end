using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class Wishlist : Entity
{
    public long TourId { get; init; }
    public long TouristId { get; init; }
}
