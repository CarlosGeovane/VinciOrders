using Moq;
using Microsoft.Extensions.Logging;
using VinciOrders.API.DTOs;
using VinciOrders.API.Models;
using VinciOrders.API.Repositories;
using VinciOrders.API.Services;

namespace VinciOrders.Tests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Mock<ILogger<OrderService>> _loggerMock;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _loggerMock = new Mock<ILogger<OrderService>>();

        _service = new OrderService(_repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_PedidoValido_RetornaPedidoCriado()
    {
        var dto = new CreateOrderDto
        {
            CustomerName = "Carlos Silva",
            Value = 150.00m
        };

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        var resultado = await _service.CreateAsync(dto);

        Assert.NotNull(resultado);
        Assert.Equal("Carlos Silva", resultado.CustomerName);
        Assert.Equal(150.00m, resultado.Value);
        Assert.NotEqual(Guid.Empty, resultado.Id);
    }

    [Fact]
    public async Task GetAllAsync_ComPedidos_RetornaListaCorreta()
    {
        var pedidos = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), CustomerName = "Carlos", Value = 100, OrderDate = DateTime.UtcNow },
            new Order { Id = Guid.NewGuid(), CustomerName = "Maria", Value = 200, OrderDate = DateTime.UtcNow }
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(pedidos);

        var resultado = await _service.GetAllAsync();

        Assert.Equal(2, resultado.Count());
    }

    [Fact]
    public async Task GetByIdAsync_IdInexistente_RetornaNull()
    {
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Order?)null);

        var resultado = await _service.GetByIdAsync(Guid.NewGuid());

        Assert.Null(resultado);
    }

    [Fact]
    public async Task GetByIdAsync_IdExistente_RetornaPedido()
    {
        var id = Guid.NewGuid();
        var pedido = new Order
        {
            Id = id,
            CustomerName = "Carlos Silva",
            Value = 150,
            OrderDate = DateTime.UtcNow
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(pedido);

        var resultado = await _service.GetByIdAsync(id);

        Assert.NotNull(resultado);
        Assert.Equal("Carlos Silva", resultado.CustomerName);
    }
}