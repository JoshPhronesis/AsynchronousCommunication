using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;

namespace Orders.Api.Messaging;

public class SqsMessenger : ISqsMessenger
{
    private readonly IAmazonSQS _amazonSqs;
    private readonly IOptions<QueueSettings> _queueSettings;
    private string? _queueUrl;

    public SqsMessenger(IAmazonSQS amazonSqs, IOptions<QueueSettings> queueSettings)
    {
        _amazonSqs = amazonSqs;
        _queueSettings = queueSettings;
    }

    public string? QueueUrl
    {
        get
        {
            if (_queueUrl != null) return _queueUrl;
            
            var queueUrl = _amazonSqs.GetQueueUrlAsync(_queueSettings.Value.QueueName).GetAwaiter().GetResult();
            _queueUrl = queueUrl.QueueUrl;

            return _queueUrl;
        }
    }

    public async Task<SendMessageResponse> SendMessageAsync<T>([DisallowNull] T message)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));

        var queueUrl = await _amazonSqs.GetQueueUrlAsync(_queueSettings.Value.QueueName);
        var request = new SendMessageRequest()
        {
            QueueUrl = queueUrl.QueueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>()
            {
                {
                    "MessageType", new MessageAttributeValue()
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await _amazonSqs.SendMessageAsync(request);
    }
}