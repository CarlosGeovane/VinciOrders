using VinciOrders.API.DTOs;

namespace VinciOrders.API.Services;

// Interface que define o "contrato" do serviço de pedidos
// Trabalha com DTOs — não expõe o Model diretamente para o Controller
public interface IOrderService
{
    // Retorna todos os pedidos como DTOs de resposta
    Task<IEnumerable<OrderResponseDto>> GetAllAsync();

    // Retorna um pedido específico — null se não existir
    Task<OrderResponseDto?> GetByIdAsync(Guid id);

    // Cria um novo pedido e retorna o resultado
    // Lança exceção se os dados forem inválidos
    Task<OrderResponseDto> CreateAsync(CreateOrderDto dto);
}