namespace Explorer.Payments.API.Dtos;

public class BundleResponseDto
{
    public long Id;
    public string Name { get; set; }
    public long AuthorId { get; set; }
    public string Status { get; set; }
}
