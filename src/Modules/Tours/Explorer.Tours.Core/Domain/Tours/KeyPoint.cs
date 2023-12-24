using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours;

public class KeyPoint : Entity
{
    public long TourId { get; init; }
    public Tour? Tour { get; init; } = null!;
    public string Name { get; init; }
    public string Description { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    public string LocationAddress { get; init; }
    public string ImagePath { get; init; }
    public long Order { get; set; }
    public bool HaveSecret { get; init; }
    public KeyPointSecret? Secret { get; private set; }
    public bool IsEncounterRequired { get; private set; }
    public bool HasEncounter { get; private set; }

    public KeyPoint(long tourId, string name, string description, double longitude, double latitude, string locationAddress, string imagePath, long order, KeyPointSecret? secret)
    {
        TourId = tourId;
        Name = name;
        Description = description;
        Longitude = longitude;
        Latitude = latitude;
        LocationAddress = locationAddress;
        ImagePath = imagePath;
        Order = order;
        HaveSecret = secret != null;
        Secret = secret;
        Validate();
    }

    public KeyPoint(long tourId, PublicKeyPoint publicKeyPoint)
    {
        TourId = tourId;
        Name = publicKeyPoint.Name;
        Description = publicKeyPoint.Description;
        Longitude = publicKeyPoint.Longitude;
        Latitude = publicKeyPoint.Latitude;
        ImagePath = publicKeyPoint.ImagePath;
        Order = publicKeyPoint.Order;
        LocationAddress = publicKeyPoint.LocationAddress;
        Validate();
    }

    private void Validate()
    {
        if (TourId == 0) throw new ArgumentException("Invalid TourId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude");
        if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude");
        if (string.IsNullOrWhiteSpace(LocationAddress)) throw new ArgumentException("Invalid Location Address");
        if (string.IsNullOrWhiteSpace(ImagePath)) throw new ArgumentException("Invalid ImagePath");
    }

    public void RequireEncounter()
    {
        IsEncounterRequired = true;
    }

    public void AddEncounter()
    {
        HasEncounter = true;
    }

    public double CalculateDistance(KeyPoint kp)
    {
        return CalculateDistance(kp.Longitude, kp.Latitude);
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

    public void HideSecret()
    {
        Secret = null;
    }
}
