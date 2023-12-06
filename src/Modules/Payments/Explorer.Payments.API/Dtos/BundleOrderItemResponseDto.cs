namespace Explorer.Payments.API.Dtos;

public class BundleOrderItemResponseDto
{
    public long Id { get; set; }
    public long BundleId { get; set; }
    public long Price { get; set; }
    public long ShoppingCartId { get; set; }
}