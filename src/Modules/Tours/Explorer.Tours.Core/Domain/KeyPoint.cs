using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class KeyPoint : Entity
{
    public long TourId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    public string ImagePath { get; init; }
    public long Order { get; init; }

    public KeyPoint(long tourId, string name, string description, double longitude, double latitude, string imagePath, long order)
    {
        TourId = tourId;
        Name = name;
        Description = description;
        Longitude = longitude;
        Latitude = latitude;
        ImagePath = imagePath;
        Order = order;
        Validate();
    }

    private void Validate()
    {
        if (TourId == 0) throw new ArgumentException("Invalid TourId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude");
        if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude");
        if (string.IsNullOrWhiteSpace(ImagePath)) throw new ArgumentException("Invalid ImagePath");
    }

    public double CalculateDistance(double longitude, double latitude)
    {
        if (longitude < -180 || longitude > 180) throw new ArgumentException("Invalid Longitude");
        if (latitude < -90 || latitude > 90) throw new ArgumentException("Invalid Latitude");

        const double earthRadius = 6371000;
        double latitude1 = this.Latitude * Math.PI / 180;
        double longitude1 = this.Longitude * Math.PI / 180;
        double latitude2 = latitude * Math.PI / 180;
        double longitude2 = longitude * Math.PI / 180;

        double latitudeDistance = latitude2 - latitude1;
        double longitudeDistance = longitude2 - longitude1;

        double a = Math.Sin(latitudeDistance / 2) * Math.Sin(latitudeDistance / 2) +
                   Math.Cos(latitude1) * Math.Cos(latitude2) *
                   Math.Sin(longitudeDistance / 2) * Math.Sin(longitudeDistance / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = earthRadius * c;

        return distance;
    }
}
