using Ardalis.GuardClauses;
using Orders.Api.Entities;
using Orders.Api.Messaging;
using Orders.Api.Repository;

namespace Orders.Api.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly ISqsMessenger _sqsMessenger;

    public OrdersService(IOrdersRepository ordersRepository,
        ISqsMessenger sqsMessenger)
    {
        _ordersRepository = ordersRepository;
        _sqsMessenger = sqsMessenger;
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        Guard.Against.Null(order);

        var result = await _ordersRepository.UpdateOrderAsync(order);
        await _sqsMessenger.SendMessageAsync(result.ToOrderUpdatedMessage());

        return result;
    }

    public Task<IEnumerable<Order>> GetOrdersAsync() => _ordersRepository.GetOrdersAsync();

    public async Task<Order> CreateOrderAsync(Order order)
    {
        Guard.Against.Null(order);

        var result = await _ordersRepository.CreateOrderAsync(order);
        await _sqsMessenger.SendMessageAsync(result.ToOrderCreatedMessage());

        return result;
    }

    public Task<Order?> GetOrderByIdAsync(Guid orderId) => _ordersRepository.GetOrderByIdAsync(orderId);

    public async Task<bool> DeleteAsync(Guid orderId)
    {
        Guard.Against.Default(orderId);

        var order = await _ordersRepository.GetOrderByIdAsync(orderId);
        if (order is null) return false;
        
        var result = await _ordersRepository.DeleteOrderByIdAsync(order);
        if (result)
        {
            await _sqsMessenger.SendMessageAsync(new OrderDeletedMessage() { OrderId = orderId });
        }

        return true;
    }
}

