namespace Explorer.Payments.API.Dtos;

public class TourSaleResponseDto
{
    public long Id { get; set; }
    public long AuthorId { get; init; }
    public string Name { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public double DiscountPercentage { get; init; }
    public ICollection<long> TourIds { get; } = new List<long>();
}
