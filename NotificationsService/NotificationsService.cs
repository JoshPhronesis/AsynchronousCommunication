using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using MediatR;
using Microsoft.Extensions.Options;
using NotificationsService.Messaging;

namespace NotificationsService;

public class NotificationsService : BackgroundService
{
    private readonly ILogger<NotificationsService> _logger;
    private readonly IAmazonSQS _amazonSqs;
    private readonly IOptions<QueueSettings> _queueSettingsOptions;
    private readonly IMediator _mediator;

    public NotificationsService(ILogger<NotificationsService> logger,
        IAmazonSQS amazonSqs,
        IOptions<QueueSettings> queueSettingsOptions,
        IMediator mediator)
    {
        _logger = logger;
        _amazonSqs = amazonSqs;
        _queueSettingsOptions = queueSettingsOptions;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrl = await _amazonSqs.GetQueueUrlAsync(_queueSettingsOptions.Value.QueueName, stoppingToken);
        if (queueUrl == null)
        {
            throw new ApplicationException("cannot find the specified queue");
        }
        
        var request = new ReceiveMessageRequest
        {
            AttributeNames = new List<string>(){"All"},
            MessageAttributeNames = new List<string>(){"All"},
            QueueUrl = queueUrl.QueueUrl,
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await _amazonSqs.ReceiveMessageAsync(request, stoppingToken);

            foreach (var message in response.Messages)
            {
                var messageType = $"NotificationsService.Messaging.{message.MessageAttributes["MessageType"].StringValue}";
                var type = Type.GetType(messageType);

                var deserializedMessage = (IMessage) JsonSerializer.Deserialize(message.Body, type!)!;

                try
                {
                    await _mediator.Send(deserializedMessage, stoppingToken);

                    await _amazonSqs.DeleteMessageAsync(new DeleteMessageRequest
                    {
                        QueueUrl = queueUrl.QueueUrl,
                        ReceiptHandle = message.ReceiptHandle
                    }, stoppingToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "an exception occurred handling the message: {message}");
                }
            }
        }
    }
}