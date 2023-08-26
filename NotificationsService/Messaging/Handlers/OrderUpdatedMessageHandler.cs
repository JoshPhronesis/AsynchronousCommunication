using MediatR;

namespace NotificationsService.Messaging.Handlers;

public class OrderUpdatedMessageHandler : IRequestHandler<OrderUpdatedMessage>
{
    private readonly INotifierService _notifierService;

    public OrderUpdatedMessageHandler(INotifierService notifierService)
    {
        _notifierService = notifierService;
    }

    public Task Handle(OrderUpdatedMessage request, CancellationToken cancellationToken)
    {
        _notifierService.Notify($"Order updated. Order Id : {request.Id}");

        return Task.CompletedTask;
    }
}