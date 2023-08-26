namespace NotificationsService.Messaging;

public class OrderDeletedMessage:IMessage
{
    public Guid OrderId { get; set; }
}