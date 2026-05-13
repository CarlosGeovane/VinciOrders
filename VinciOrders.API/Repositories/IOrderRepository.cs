using VinciOrders.API.Models;

namespace VinciOrders.API.Repositories;

// Interface que define o "contrato" do repositório
// Qualquer repositório de pedidos DEVE implementar esses métodos
// Isso facilita testes e futuras trocas de banco de dados
public interface IOrderRepository
{
    // Retorna todos os pedidos do banco
    Task<IEnumerable<Order>> GetAllAsync();

    // Retorna um pedido específico pelo Id
    Task<Order?> GetByIdAsync(Guid id);

    // Salva um novo pedido no banco
    Task AddAsync(Order order);
}