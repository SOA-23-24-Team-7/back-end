namespace Explorer.Tours.API.Dtos;

public class TourDurationResponseDto
{
    public int Duration { get; set; }
    public TransportType TransportType { get; set; }
}

public enum TransportType
{
    Walking,
    Bicycle,
    Car
}
