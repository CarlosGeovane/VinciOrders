using System.ComponentModel.DataAnnotations;

namespace VinciOrders.API.DTOs;

public class CreateOrderDto
{
    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor do pedido é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pedido deve ser maior que zero.")]
    public decimal? Value { get; set; }
}