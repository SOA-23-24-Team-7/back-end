using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Payments.Core.Domain.ShoppingCarts;

namespace Explorer.Payments.Core.Domain;

public class TourSale : Entity
{
    public long AuthorId { get; init; }
    public string Name { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public double DiscountPercentage { get; init; }
    public ICollection<long> TourIds { get; init; }

    public TourSale(long authorId, string name, DateOnly startDate, DateOnly endDate, double discountPercentage, ICollection<long> tourIds)
    {
        AuthorId = authorId;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        DiscountPercentage = discountPercentage;
        TourIds = tourIds ?? new List<long>();
        Validate();
    }

    private void Validate()
    {
        if (AuthorId == 0) throw new ArgumentException("Invalid author id.");
        if (string.IsNullOrEmpty(Name)) throw new ArgumentException("Invalid name.");
        if (StartDate > EndDate || StartDate.AddDays(14) < EndDate) throw new ArgumentException("Invalid date range.");
        if (DiscountPercentage <= 0.0 || DiscountPercentage > 1.0) throw new ArgumentException("Invalid discount percentage.");
        if (TourIds.Count == 0 || TourIds.Contains(0)) throw new ArgumentException("Invalid tour ids.");
    }
}
