using VinciOrders.API.DTOs;
using VinciOrders.API.Models;
using VinciOrders.API.Repositories;

namespace VinciOrders.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository repository, ILogger<OrderService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
    {
        _logger.LogInformation("Buscando todos os pedidos.");

        var orders = await _repository.GetAllAsync();

        _logger.LogInformation("Total de pedidos encontrados: {Total}", orders.Count());

        return orders.Select(order => new OrderResponseDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Value = order.Value,
            OrderDate = order.OrderDate
        });
    }

    public async Task<OrderResponseDto?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando pedido com Id: {Id}", id);

        var order = await _repository.GetByIdAsync(id);

        if (order == null)
        {
            _logger.LogWarning("Pedido com Id {Id} não encontrado.", id);
            return null;
        }

        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Value = order.Value,
            OrderDate = order.OrderDate
        };
    }

    public async Task<OrderResponseDto> CreateAsync(CreateOrderDto dto)
    {
        _logger.LogInformation("Criando pedido para o cliente: {CustomerName}", dto.CustomerName);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = dto.CustomerName,
            Value = dto.Value!.Value,
            OrderDate = DateTime.UtcNow
        };

        await _repository.AddAsync(order);

        _logger.LogInformation("Pedido criado com sucesso. Id: {Id}", order.Id);

        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Value = order.Value,
            OrderDate = order.OrderDate
        };
    }
}