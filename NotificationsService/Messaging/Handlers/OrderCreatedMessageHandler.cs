using MediatR;

namespace NotificationsService.Messaging.Handlers;

public class OrderCreatedMessageHandler : IRequestHandler<OrderCreatedMessage>
{
    private readonly INotifierService _notifierService;

    public OrderCreatedMessageHandler(INotifierService notifierService)
    {
        _notifierService = notifierService;
    }

    public Task Handle(OrderCreatedMessage request, CancellationToken cancellationToken)
    {
        _notifierService.Notify($"Order created. Order Id : {request.Id}");

        return Task.CompletedTask;
    }
}