namespace Orders.Api.Services;

public class OrderDeletedMessage
{
    public Guid OrderId { get; set; }
}