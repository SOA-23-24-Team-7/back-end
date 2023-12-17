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
    public double Distance { get; init; }
    public DateTime? PublishDate { get; private set; }
    public DateTime? ArchiveDate { get; private set; }
    public ICollection<Equipment> EquipmentList { get; init; } = new List<Equipment>();

    [InverseProperty("Tour")]
    public ICollection<KeyPoint>? KeyPoints { get; } = new List<KeyPoint>();
    public ICollection<TourDuration>? Durations { get; } = new List<TourDuration>();
    public ICollection<Review>? Reviews { get; init; }
    public TourCategory Category { get; private set; }
    public Tour(long authorId, string name, string description, TourCategory category, List<string> tags, int difficulty = 1, DateTime? archiveDate = null, DateTime? publishDate = null, double distance = 0, TourStatus status = TourStatus.Draft, double price = 0, bool isDeleted = false)
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
        ArchiveDate = archiveDate;
        Category = category;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (Difficulty < 1 || Difficulty > 5) throw new ArgumentException("Invalid Difficulty");
        //if (Tags.Count == 0) throw new ArgumentNullException("Tags cannot be empty");
        if (Price < 0) throw new ArgumentException("Price cannot be negative");
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

    public bool Archive(long authorId)
    {
        if (Status == TourStatus.Published && AuthorId == authorId)
        {
            ArchiveDate = DateTime.UtcNow;
            Status = TourStatus.Archived;

            return true;
        }

        return false;
    }

    public double GetAverageRating()
    {
        if (Reviews == null || Reviews.Count == 0) return 0;
        return Reviews.Average(r => r.Rating);
    }

    public string GetStatusName()
    {
        return Status.ToString().ToLower();
    }

    public double CalculateLength()
    {
        double length = 0;

        for (int i = 0; i <= KeyPoints.Count - 2; ++i)
        {
            var kp1 = KeyPoints.ElementAt(i);
            var kp2 = KeyPoints.ElementAt(i + 1);

            length += kp1.CalculateDistance(kp2.Longitude, kp2.Latitude);
        }

        return length;
    }

    public KeyPoint GetPreviousKeyPoint(KeyPoint keyPoint)
    {
        if (!KeyPoints.Contains(keyPoint)) throw new ArgumentException("Key point not in tour.");

        if (KeyPoints == KeyPoints.ElementAt(0)) return null;

        var previous = KeyPoints.ElementAt(0);
        foreach (var kp in KeyPoints)
        {
            if (kp == keyPoint)
            {
                break;
            }
            previous = kp;
        }

        return previous;
    }

    // Dodala najlepsa devojka na svetu
    public bool MarkAsReady(long authorId)
    {
        if (Status == TourStatus.Draft && AuthorId == authorId && KeyPoints.Count >= 2)
        {
            Status = TourStatus.Ready;

            return true;
        }

        return false;
    }
}

public enum TourStatus
{
    Draft,
    Published,
    Archived,
    Ready
}
public enum TourCategory
{
    Adventure,
    FamilyTrips,
    Cruise,
    Cultural
}
