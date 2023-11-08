using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Tours.Core.Domain.Tours;

public class TourDuration : ValueObject
{
    public int Duration { get; private set; }
    public TransportType TransportType { get; private set; }

    [JsonConstructor]
    public TourDuration(int duration, TransportType transportType)
    {
        Duration = duration;
        TransportType = transportType;
    }

    public void SetDuration(int duration)
    {
        Duration = duration;
    }

    public void SetTransportType(TransportType transportType)
    {
        TransportType = transportType;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Duration;
        yield return TransportType;
    }
}

public enum TransportType
{
    Walking,
    Bicycle,
    Car
}