namespace VinciOrders.API.Models;
public class Order
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public decimal Value { get; set; }

    public DateTime OrderDate { get; set; }
}