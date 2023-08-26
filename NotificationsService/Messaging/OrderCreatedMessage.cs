namespace NotificationsService.Messaging;

public class OrderCreatedMessage : IMessage
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string CustomerEmail { get; set; }
}