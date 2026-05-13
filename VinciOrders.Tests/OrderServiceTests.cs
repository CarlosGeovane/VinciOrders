using Moq;
using Microsoft.Extensions.Logging;
using VinciOrders.API.DTOs;
using VinciOrders.API.Models;
using VinciOrders.API.Repositories;
using VinciOrders.API.Services;

namespace VinciOrders.Tests;

// Classe de testes do OrderService
// Cada método testa um comportamento específico do serviço
public class OrderServiceTests
{
    // Mock simula o repositório sem precisar de banco de dados real
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Mock<ILogger<OrderService>> _loggerMock;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _loggerMock = new Mock<ILogger<OrderService>>();

        // Cria o serviço com as dependências simuladas
        _service = new OrderService(_repositoryMock.Object, _loggerMock.Object);
    }

    // Teste 1: Criar pedido válido deve retornar o pedido criado
    [Fact]
    public async Task CreateAsync_PedidoValido_RetornaPedidoCriado()
    {
        // Arrange — prepara os dados do teste
        var dto = new CreateOrderDto
        {
            CustomerName = "Carlos Silva",
            Value = 150.00m
        };

        // Simula o repositório salvando sem erro
        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Order>()))
            .Returns(Task.CompletedTask);

        // Act — executa o que está sendo testado
        var resultado = await _service.CreateAsync(dto);

        // Assert — verifica se o resultado é o esperado
        Assert.NotNull(resultado);
        Assert.Equal("Carlos Silva", resultado.CustomerName);
        Assert.Equal(150.00m, resultado.Value);
        Assert.NotEqual(Guid.Empty, resultado.Id);
    }

    // Teste 2: Buscar todos os pedidos deve retornar a lista correta
    [Fact]
    public async Task GetAllAsync_ComPedidos_RetornaListaCorreta()
    {
        // Arrange — simula o banco retornando dois pedidos
        var pedidos = new List<Order>
        {
            new Order { Id = Guid.NewGuid(), CustomerName = "Carlos", Value = 100, OrderDate = DateTime.UtcNow },
            new Order { Id = Guid.NewGuid(), CustomerName = "Maria", Value = 200, OrderDate = DateTime.UtcNow }
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(pedidos);

        // Act
        var resultado = await _service.GetAllAsync();

        // Assert
        Assert.Equal(2, resultado.Count());
    }

    // Teste 3: Buscar pedido por Id inexistente deve retornar null
    [Fact]
    public async Task GetByIdAsync_IdInexistente_RetornaNull()
    {
        // Arrange — simula o banco não encontrando nenhum pedido
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Order?)null);

        // Act
        var resultado = await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(resultado);
    }

    // Teste 4: Buscar pedido por Id existente deve retornar o pedido
    [Fact]
    public async Task GetByIdAsync_IdExistente_RetornaPedido()
    {
        // Arrange
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

        // Act
        var resultado = await _service.GetByIdAsync(id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Carlos Silva", resultado.CustomerName);
    }
}