namespace Orders.Api.Services;

public class OrderUpdatedMessage
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string CustomerEmail { get; set; }
}