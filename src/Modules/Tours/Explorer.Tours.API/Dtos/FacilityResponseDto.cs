namespace Explorer.Tours.API.Dtos;

public class FacilityResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int AuthorId { get; set; }
    public FacilityCategory Category { get; set; }
    public double GeographicalWidth { get; set; }
    public double GeographicalHeight { get; set; }
}

public enum FacilityCategory
{
    Restaurant,
    ParkingLot,
    Toilet,
    Hospital,
    Cafe,
    Pharmacy,
    ExchangeOffice,
    BusStop,
    Shop,
    Other
}
