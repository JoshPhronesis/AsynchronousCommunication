using Orders.Api.Entities;

namespace Orders.Api.Services;

public static class Extensions
{
    public static OrderUpdatedMessage ToOrderUpdatedMessage(this Order order)
    {
        return new OrderUpdatedMessage
        {
            Id = order.Id,
            Amount = order.Amount,
            Currency = order.Currency,
            CustomerEmail = order.CustomerEmail
        };
    }  
    
    public static OrderCreatedMessage ToOrderCreatedMessage(this Order order)
    {
        return new OrderCreatedMessage
        {
            Id = order.Id,
            Amount = order.Amount,
            Currency = order.Currency,
            CustomerEmail = order.CustomerEmail
        };
    }
    
    
}