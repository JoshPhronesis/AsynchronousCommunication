using Orders.Api.Entities;

namespace Orders.Api.Services;

public interface IOrdersService
{
    Task<Order> UpdateOrderAsync(Order order);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> CreateOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(Guid orderId);
    Task<bool> DeleteAsync(Guid orderId);
}