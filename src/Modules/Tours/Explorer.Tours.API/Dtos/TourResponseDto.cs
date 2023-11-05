namespace Explorer.Tours.API.Dtos;

public class TourResponseDto
{
    public int Id { get; set; }
    public long AuthorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Difficulty { get; set; }
    public List<string> Tags { get; set; }
    public TourStatus Status { get; set; }
    public double Price { get; set; }
    public bool IsDeleted { get; set; }
    public double Distance { get; set; }
    public List<KeyPointDto> KeyPoints { get; set; }
}
public enum TourStatus
{
    Draft,
    Published,
    Archived
}
