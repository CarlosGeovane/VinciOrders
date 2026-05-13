using System.ComponentModel.DataAnnotations;

namespace VinciOrders.API.DTOs;

// DTO de entrada: representa os dados que o cliente envia para criar um pedido
public class CreateOrderDto
{
    // [Required] garante que o campo não pode ser vazio ou nulo
    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    public string CustomerName { get; set; } = string.Empty;

    // [Range] garante que o valor deve ser maior que zero
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pedido deve ser maior que zero.")]
    public decimal Value { get; set; }
}