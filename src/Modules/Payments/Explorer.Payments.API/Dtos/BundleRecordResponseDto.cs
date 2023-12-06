namespace Explorer.Payments.API.Dtos;

public class BundleRecordResponseDto
{
    public long Id { get; set; }
    public long TouristId { get; set; }
    public long BundleId { get; set; }
    public string BundleName { get; set; }
    public double Price { get; set; }
    public DateTime PurchasedDate { get; set; }
}
