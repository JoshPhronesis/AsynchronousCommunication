using Orders.Api.Entities;

namespace Orders.Api.Repository;

public interface IOrdersRepository
{
    Task<Order> CreateOrderAsync(Order entity);
    Task<Order> UpdateOrderAsync(Order entity);
    Task<Order?> GetOrderByIdAsync(Guid orderId);
    Task<bool> DeleteOrderByIdAsync(Order entity);
    Task<IEnumerable<Order>> GetOrdersAsync();
}