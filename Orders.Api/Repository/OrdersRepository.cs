using Ardalis.GuardClauses;
using Orders.Api.Data;
using Orders.Api.Entities;

namespace Orders.Api.Repository;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _dbContext;

    public OrdersRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order> CreateOrderAsync(Order entity)
    {
        Guard.Against.Null(entity);
        
        await _dbContext.Orders.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<Order> UpdateOrderAsync(Order entity)
    {
        Guard.Against.Null(entity);
        
        _dbContext.Orders.Update(entity);
        await _dbContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task<Order?> GetOrderByIdAsync(Guid orderId)
    {
        var result = await _dbContext.Orders.FindAsync(orderId);
        
        return result;
    }

    public async Task<bool> DeleteOrderByIdAsync(Order entity)
    {
        Guard.Against.Null(entity);

        _dbContext.Orders.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return Task.FromResult(_dbContext.Orders.AsEnumerable());
    }
}