namespace Explorer.Payments.API.Dtos;

public class BundleRecordResponseDto
{
    public long Id { get; set; }
    public long TouristId { get; init; }
    public long BundleId { get; init; }
    public double Price { get; set; }
    public DateTime PurchasedDate { get; init; }
}
