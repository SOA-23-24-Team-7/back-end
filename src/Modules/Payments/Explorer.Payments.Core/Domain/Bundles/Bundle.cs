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
    public long Price { get; init; }
    public BundleStatus Status { get; private set; }
    public List<BundleItem> BundleItems { get; init; }

    public Bundle()
    {
        BundleItems = new List<BundleItem>();
    }

    public Bundle(string name, long price, BundleStatus status) : this()
    {
        if (price < 0) throw new ArgumentException("Invalid price.");
        if (name  == null || name == "") throw new ArgumentException("Invalid name.");

        Name = name;
        Price = price;
        Status = status;
    }

    public void AddBundleItem(int tourId)
    {
        var bundleItem = new BundleItem(Id, tourId);
        BundleItems.Add(bundleItem);
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