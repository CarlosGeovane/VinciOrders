using Microsoft.EntityFrameworkCore;
using VinciOrders.API.Data;
using VinciOrders.API.Models;

namespace VinciOrders.API.Repositories;

// Implementação concreta do repositório
// Aqui é onde o Entity Framework acessa o banco de dados de verdade
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    // O DbContext é injetado automaticamente pelo .NET (injeção de dependência)
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    // Busca todos os pedidos na tabela Orders
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    // Busca um pedido pelo Id — retorna null se não encontrar
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    // Adiciona um novo pedido e salva no banco
    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}