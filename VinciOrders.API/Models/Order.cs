namespace VinciOrders.API.Models;

// Modelo que representa um pedido no banco de dados
// Esta classe é espelhada como uma tabela pelo Entity Framework
public class Order
{
    // Identificador único gerado automaticamente (nunca se repete)
    public Guid Id { get; set; }

    // Nome do cliente que realizou o pedido (obrigatório)
    public string CustomerName { get; set; } = string.Empty;

    // Valor do pedido (deve ser maior que zero)
    public decimal Value { get; set; }

    // Data e hora em que o pedido foi registrado
    public DateTime OrderDate { get; set; }
}