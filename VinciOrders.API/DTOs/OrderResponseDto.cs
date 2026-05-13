namespace VinciOrders.API.DTOs;

// DTO de saída: representa os dados que a API devolve ao cliente
// Inclui todos os campos, inclusive Id e Data gerados pelo sistema
public class OrderResponseDto
{
    // Identificador único do pedido
    public Guid Id { get; set; }

    // Nome do cliente
    public string CustomerName { get; set; } = string.Empty;

    // Valor do pedido
    public decimal Value { get; set; }

    // Data em que o pedido foi registrado
    public DateTime OrderDate { get; set; }
}