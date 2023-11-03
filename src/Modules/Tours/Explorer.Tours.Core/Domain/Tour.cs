using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Tour : Entity
{
    public long AuthorId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Difficulty { get; init; }
    public List<string> Tags { get; init; }
    public TourStatus Status { get; init; }
    public double Price { get; init; }
    public bool IsDeleted { get; init; }


    public ICollection<Equipment> EquipmentList { get; init; }

    public Tour(long authorId, string name, string description, int difficulty, List<string> tags, TourStatus status = TourStatus.Draft, double price = 0, bool isDeleted = false)
    {
        AuthorId = authorId;
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Tags = tags;
        Status = status;
        Price = price;
        IsDeleted = isDeleted;
        Validate();

    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (Difficulty < 1 || Difficulty > 5) throw new ArgumentException("Invalid Difficulty");
        if (Tags.Count == 0) throw new ArgumentNullException("Tags cannot be empty");
        if (Price < 0) throw new ArgumentException("Price cannot be negative");
    }

    public string GetStatusName()
    {
        return Status.ToString().ToLower();
    }

}
public enum TourStatus
{
    Draft,
    Published
}
