using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours;

public class KeyPoint : Entity
{
    public long TourId { get; init; }
    public Tour? Tour { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    public string LocationAddress { get; init; }
    public string ImagePath { get; init; }
    public long Order { get; init; }

    public KeyPoint(long tourId, string name, string description, double longitude, double latitude, string locationAddress, string imagePath, long order)
    {
        TourId = tourId;
        Name = name;
        Description = description;
        Longitude = longitude;
        Latitude = latitude;
        LocationAddress = locationAddress;
        ImagePath = imagePath;
        Order = order;
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
        Order= publicKeyPoint.Order;
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
}

