using System.Reflection;
using Amazon.SQS;
using NotificationsService;
using NotificationsService.Messaging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<NotificationsService.NotificationsService>();
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection("QueueSettings"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSingleton<INotifierService, ConsoleNotifierService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();