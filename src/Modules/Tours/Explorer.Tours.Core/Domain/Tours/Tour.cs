using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.Core.Domain.Tours;
public class Tour : Entity
{
    public long AuthorId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Difficulty { get; init; }
    public List<string> Tags { get; init; }
    public TourStatus Status { get; private set; }
    public double Price { get; init; }
    public bool IsDeleted { get; init; }

    //polje za duzinu NAPISAO MALI NINOSLAV
    public double Distance { get; init; }
    public DateTime? PublishDate { get; private set; }
    public ICollection<Equipment> EquipmentList { get; init; }

    [InverseProperty("Tour")]
    public ICollection<KeyPoint> KeyPoints { get; } = new List<KeyPoint>();

    //Lista za trajanje ture DODALA NAJVECA LEGENDA NA SVETU (VUKASIN)
    public ICollection<TourDuration> Durations { get; } = new List<TourDuration>();

    public Tour(long authorId, string name, string description, int difficulty, List<string> tags, DateTime? publishDate = null, double distance = 0, TourStatus status = TourStatus.Draft, double price = 0, bool isDeleted = false)
    {
        AuthorId = authorId;
        Name = name;
        Description = description;
        Difficulty = difficulty;
        Tags = tags;
        Status = status;
        Price = price;
        IsDeleted = isDeleted;
        Distance = distance;
        PublishDate = publishDate;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (Difficulty < 1 || Difficulty > 5) throw new ArgumentException("Invalid Difficulty");
        if (Tags.Count == 0) throw new ArgumentNullException("Tags cannot be empty");
        if (Price < 0) throw new ArgumentException("Price cannot be negative");
        //if (Distance < 0) throw new ArgumentException("Distance cannot be negative");
    }

    public bool Publish(long authorId)
    {
        if (ValidationForPublishing(authorId))
        {
            PublishDate = DateTime.UtcNow;
            Status = TourStatus.Published;

            return true;
        }

        return false;
    }

    public bool ValidationForPublishing(long authorId)
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description) || Difficulty < 1 || Difficulty > 5 ||
            Tags.Count == 0 || KeyPoints == null || KeyPoints.Count < 2 || Durations.Count < 1 || AuthorId != authorId)
        {
            return false;
        }

        return true;
    }

    public string GetStatusName()
    {
        return Status.ToString().ToLower();
    }
}

public enum TourStatus
{
    Draft,
    Published,
    Archived
}
