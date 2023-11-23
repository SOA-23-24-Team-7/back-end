namespace Explorer.Payments.API.Dtos;

public class OrderItemCreateDto
{
    public long TourId { get; set; }
    public string TourName { get; set; }
    public double Price { get; set; }
    public long ShoppingCartId { get; set; }
}
