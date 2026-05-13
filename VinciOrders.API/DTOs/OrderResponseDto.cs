namespace VinciOrders.API.DTOs;
public class OrderResponseDto
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public DateTime OrderDate { get; set; }
}