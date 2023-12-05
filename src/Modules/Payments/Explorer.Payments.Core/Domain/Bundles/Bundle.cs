using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Bundles;

public class Bundle : Entity
{
    public string Name
    {
        get {  return Name; }
        set
        {
            if (value == "" && value == null) throw new ArgumentException("Invalid name.");
            Name = value;
        }
    }
    public long Price
    {
        get { return Price; }
        set
        {
            if (value < 0) throw new ArgumentException("Invalid price.");
            Price = value;
        }
    }
    public long AuthorId { get; init; }
    public BundleStatus Status { get; private set; }
    public List<BundleItem> BundleItems { get; init; }

    public Bundle()
    {
        BundleItems = new List<BundleItem>();
    }

    public Bundle(string name, long price, long authorId, BundleStatus status) : this()
    {
        Name = name;
        Price = price;
        AuthorId = authorId;
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