namespace Explorer.Payments.API.Dtos;

public class ShoppingCartUpdateDto
{
    public long Id { get; set; }
    public long TouristId { get; set; }
    public double TotalPrice { get; set; }
    public bool IsPurchased { get; set; }
    public List<OrderItemResponseDto> OrderItems { get; set; }
}
