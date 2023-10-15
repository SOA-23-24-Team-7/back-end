using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Facility : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; } // for now it will be a url, so string type
    public int AuthorId { get; init; } // for now i will save just an id, will change if needed
    public FacilityCategory Category { get; init; }

    public Facility(string name, string? description, string? imageUrl, int authorId, FacilityCategory category)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Category = category;
        AuthorId = authorId;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
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