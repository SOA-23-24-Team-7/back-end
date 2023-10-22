using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Facility : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; } 
    public int AuthorId { get; init; } 
    public double GeographicalWidth { get; init; }
    public double GeographicalHeight { get; init; }
    public FacilityCategory Category { get; init; }

    public Facility(string name, string? description, string? imageUrl, int authorId, FacilityCategory category, double geographicalWidth, double geographicalHeight)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Category = category;
        AuthorId = authorId;
        GeographicalWidth = geographicalWidth;
        GeographicalHeight = geographicalHeight;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(Category.ToString())) throw new ArgumentException("Invalid Category.");
        if (AuthorId < 0) throw new ArgumentException("Invalid Author Id.");
    }

    public string GetCategoryName()
    {
        return Category.ToString();
    }
}

public enum FacilityCategory
{
    Restaurant,
    ParkingLot,
    Toilet,
    Hospital,
    Cafe,
    Pharmacy,
    ExchangeOffice,
    BusStop,
    Shop,
    Other
}