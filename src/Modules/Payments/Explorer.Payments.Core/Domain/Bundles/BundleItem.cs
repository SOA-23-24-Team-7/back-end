using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
