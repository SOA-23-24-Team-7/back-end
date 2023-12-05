using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Bundles;

public class Bundle : Entity
{
    public string Name { get; init; }
    public int Price { get; init; }
    public BundleStatus Status { get; private set; }
    public List<BundleItem> BundleItems { get; init; }

    public Bundle() { }

    public Bundle(string name, int price, BundleStatus status, List<BundleItem> bundleItems)
    {
        if (price < 0) throw new ArgumentException("Invalid price.");
        if (name  == null || name == "") throw new ArgumentException("Invalid name.");

        Name = name;
        Price = price;
        Status = status;
        BundleItems = bundleItems;
    }

    public void Publish()
    {
        if (Status != BundleStatus.Deleted) Status = BundleStatus.Published;
    }

    public void Delete()
    {
        if (Status != BundleStatus.Published) Status = BundleStatus.Deleted;
    }

    public void Archive()
    {
        if (Status == BundleStatus.Published) Status = BundleStatus.Archived;
    }
}

public enum BundleStatus
{
    Draft,
    Published,
    Archived,
    Deleted
}