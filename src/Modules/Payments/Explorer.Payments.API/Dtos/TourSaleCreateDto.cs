namespace Explorer.Payments.API.Dtos;

public class TourSaleCreateDto
{
    public long AuthorId { get; set; }
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public double DiscountPercentage { get; set; }
    public ICollection<long> TourIds { get; set; } = new List<long>();
}
