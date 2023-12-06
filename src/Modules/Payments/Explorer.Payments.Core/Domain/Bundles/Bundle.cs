using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain.Bundles;

public class Bundle : Entity
{
    public string Name { get; private set; }
    public long Price { get; private set; }
    public long AuthorId { get; init; }
    public BundleStatus Status { get; private set; }
    public List<BundleItem> BundleItems { get; init; }

    public Bundle()
    {
        BundleItems = new List<BundleItem>();
    }

    public Bundle(string name, long price, long authorId, BundleStatus status) : this()
    {
        Rename(name);
        ChangePrice(price);
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

    public void Rename(string name)
    {
        if (name == "" || name == null) throw new ArgumentException("Invalid name.");
        Name = name;
    }

    public void ChangePrice(long price)
    {
        if (price < 0) throw new ArgumentException("Invalid price.");
        Price = price;
    }
}

public enum BundleStatus
{
    Draft,
    Published,
    Archived,
    Deleted
}