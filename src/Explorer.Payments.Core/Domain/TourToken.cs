using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain;

public class TourToken : Entity
{
    public long TourId { get; init; }
    public long TouristId { get; init; }
}
