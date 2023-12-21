namespace Explorer.Tours.API.Dtos;

public class TourResponseDto
{
    public long Id { get; set; }
    public long? AuthorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Difficulty { get; set; }
    public List<string> Tags { get; set; }
    public TourStatus Status { get; set; }
    public double Price { get; set; }
    public bool IsDeleted { get; set; }
    public double Distance { get; set; }
    public double? AverageRating { get; set; }
    public List<KeyPointResponseDto> KeyPoints { get; set; }
    public List<TourDurationResponseDto> Durations { get; set; }
    public DateTime PublishDate { get; set; }
    public DateTime ArchiveDate { get; set; }
    public TourCategory Category { get; set; }
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

