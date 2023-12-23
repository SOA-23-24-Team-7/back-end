namespace Explorer.Encounters.API.Dtos;

public class SocialEncounterResponseDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public double Radius { get; set; }
    public int XpReward { get; set; }
    public EncounterStatus Status { get; set; }
    public int PeopleNumber {  get; set; }
}
