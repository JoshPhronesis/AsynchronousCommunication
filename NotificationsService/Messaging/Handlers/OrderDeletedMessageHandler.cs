using MediatR;

namespace NotificationsService.Messaging.Handlers;

public class OrderDeletedMessageHandler : IRequestHandler<OrderDeletedMessage>
{
    private readonly INotifierService _notifierService;

    public OrderDeletedMessageHandler(INotifierService notifierService)
    {
        _notifierService = notifierService;
    }

    public Task Handle(OrderDeletedMessage request, CancellationToken cancellationToken)
    {
        _notifierService.Notify($"Order deleted. Order Id : {request.OrderId}");

        return Task.CompletedTask;
    }
}