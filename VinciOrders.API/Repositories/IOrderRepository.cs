using VinciOrders.API.Models;

namespace VinciOrders.API.Repositories;
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(Guid id);

    Task AddAsync(Order order);
}