using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class TouristPosition : Entity
{
    public long TouristId { get; init; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public TouristPosition(long touristId, double longitude, double latitude)
    {
        TouristId = touristId;
        Longitude = longitude;
        Latitude = latitude;
        Validate();
    }

    private void Validate()
    {
        if (TouristId == 0) throw new ArgumentException("Invalid TourId");
        if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude");
        if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude");
    }
}
