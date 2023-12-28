namespace Explorer.Encounters.API.Dtos;

public class KeyPointEncounterCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public double Radius { get; set; }
    public int XpReward { get; set; }
    public long KeyPointId { get; set; }
    public bool IsRequired { get; set; }
}