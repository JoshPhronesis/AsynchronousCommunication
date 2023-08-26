using Amazon.SQS.Model;

namespace NotificationsService.Messaging;

public interface ISqsMessenger
{
    Task<SendMessageResponse> SendMessageAsync<T>(T message);
}