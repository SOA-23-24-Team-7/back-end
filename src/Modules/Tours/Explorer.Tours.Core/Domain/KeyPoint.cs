using Explorer.BuildingBlocks.Core.Domain;
using System.Net.Mail;

namespace Explorer.Tours.Core.Domain;

public class KeyPoint : Entity
{
    public long TourId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    public string ImagePath { get; init; }

    public KeyPoint(long tourId, string name, string description, double longitude, double latitude, string imagePath)
    {
        TourId = tourId;
        Name = name;
        Description = description;
        Longitude = longitude;
        Latitude = latitude;
        ImagePath = imagePath;
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
}
