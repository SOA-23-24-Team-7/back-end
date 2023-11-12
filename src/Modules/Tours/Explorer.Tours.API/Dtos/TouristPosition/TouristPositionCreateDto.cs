namespace Explorer.Tours.API.Dtos.TouristPosition;

public class TouristPositionCreateDto
{
    public long TouristId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}
