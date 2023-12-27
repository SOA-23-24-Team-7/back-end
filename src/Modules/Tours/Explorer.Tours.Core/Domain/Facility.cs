using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Facility : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? ImagePath { get; init; }
    public int AuthorId { get; init; }
    public FacilityCategory Category { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }

    public Facility(string name, string? description, string? imagePath, int authorId, FacilityCategory category, double longitude, double latitude)
    {
        Name = name;
        Description = description;
        ImagePath = imagePath;
        Category = category;
        AuthorId = authorId;
        Longitude = longitude;
        Latitude = latitude;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
        if (string.IsNullOrWhiteSpace(Category.ToString())) throw new ArgumentException("Invalid Category.");
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