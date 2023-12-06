using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.Bundles;

public class BundleItem : Entity
{
    public long BundleId { get; init; }
    public long TourId { get; init; }

    public BundleItem() { }

    public BundleItem(long bundleId, long tourId)
    {
        BundleId = bundleId;
        TourId = tourId;
    }
}
