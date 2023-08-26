using Amazon.SQS.Model;

namespace Orders.Api.Messaging;

public interface ISqsMessenger
{
    Task<SendMessageResponse> SendMessageAsync<T>(T message);
}