using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Tour : Entity
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int Difficulty { get; init; }
    public List<string> Tags { get; init; }
    public TourStatus Status { get; init; }
    public double Price { get; init; }
    public bool IsDeleted { get; init; }

    public Tour(string name, string description, int difficulty, List<string> tags)
    {
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Tags = tags;
        Status = TourStatus.Draft;
        Price = 0;
        IsDeleted = false;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (Difficulty < 1 || Difficulty > 5) throw new ArgumentException("Invalid Difficulty");
        if (Tags == null) throw new ArgumentNullException("Tags cannot be null");
        if (Price < 0) throw new ArgumentException("Price cannot be negative");
    }

}
public enum TourStatus
{
    Draft,
    Published
}
