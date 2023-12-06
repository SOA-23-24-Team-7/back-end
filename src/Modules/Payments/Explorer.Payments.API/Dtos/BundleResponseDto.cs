namespace Explorer.Payments.API.Dtos;

public class BundleResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Price { get; set; }
    public long AuthorId { get; set; }
    public BundleStatus Status { get; set; }
    public List<BundleItemResponseDto> BundleItems { get; set; }
}

public enum BundleStatus
{
    Draft,
    Published,
    Archived,
    Deleted
}
