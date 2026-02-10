using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.EventBus.Abstractions;
using Shared.EventBus.Events;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace Shared.EventBusRabbitMQ
{
    public sealed class RabbitMQEventBus(
     ILogger<RabbitMQEventBus> logger,
     IServiceProvider serviceProvider,
     IOptions<EventBusOptions> options,
     IOptions<EventBusSubscriptionInfo> subscriptionOptions,
     IOptions<RabbitMqOptions> rabbitOptions) : IEventBus, IDisposable, IHostedService
    {
        private const string ExchangeName = "CoreBanking_event_bus";
        private readonly RabbitMqOptions _rabbitOptions = rabbitOptions.Value;
        private readonly string _queueName = options.Value.SubscriptionClientName;
        private readonly EventBusSubscriptionInfo _subscriptionInfo = subscriptionOptions.Value;
        private IConnection _rabbitMQConnection;

        private IChannel _consumerChannel;

        public async Task PublishAsync(IntegrationEvent @event)
        {
            var routingKey = @event.GetType().Name;

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, routingKey);
            }

            using var channel = (await _rabbitMQConnection?.CreateChannelAsync()) ?? throw new InvalidOperationException("RabbitMQ connection is not open");

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);
            }

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: "direct");

            var body = SerializeMessage(@event);

            var properties = new BasicProperties()
            {
                DeliveryMode = DeliveryModes.Persistent
            };


            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);
            }

            try
            {
                await channel.BasicPublishAsync(
                    exchange: ExchangeName,
                    routingKey: routingKey,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to publish event {EventId} to RabbitMQ", @event.Id);
            }
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();
        }

        private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            static IEnumerable<string> ExtractTraceContextFromBasicProperties(IReadOnlyBasicProperties props, string key)
            {
                if (props.Headers.TryGetValue(key, out var value))
                {
                    var bytes = value as byte[];
                    return [Encoding.UTF8.GetString(bytes)];
                }
                return [];
            }

            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {

                if (message.Contains("throw-fake-exception", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);

            }

            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
            // For more information see: https://www.rabbitmq.com/dlx.html
            await _consumerChannel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);
            }

            await using var scope = serviceProvider.CreateAsyncScope();

            if (!_subscriptionInfo.EventTypes.TryGetValue(eventName, out var eventType))
            {
                logger.LogWarning("Unable to resolve event type for event name {EventName}", eventName);
                return;
            }

            // Deserialize the event
            var integrationEvent = DeserializeMessage(message, eventType);

            // REVIEW: This could be done in parallel

            // Get all the handlers using the event type as the key
            foreach (var handler in scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType))
            {
                await handler.Handle(integrationEvent);
            }
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode",
            Justification = "The 'JsonSerializer.IsReflectionEnabledByDefault' feature switch, which is set to false by default for trimmed .NET apps, ensures the JsonSerializer doesn't use Reflection.")]
        [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "See above.")]
        private IntegrationEvent DeserializeMessage(string message, Type eventType)
        {
            return JsonSerializer.Deserialize(message, eventType, _subscriptionInfo.JsonSerializerOptions) as IntegrationEvent;
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode",
            Justification = "The 'JsonSerializer.IsReflectionEnabledByDefault' feature switch, which is set to false by default for trimmed .NET apps, ensures the JsonSerializer doesn't use Reflection.")]
        [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "See above.")]
        private byte[] SerializeMessage(IntegrationEvent @event)
        {
            return JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), _subscriptionInfo.JsonSerializerOptions);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Messaging is async so we don't need to wait for it to complete.
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    logger.LogInformation("Starting RabbitMQ connection on a background thread");

                    var factory = new ConnectionFactory
                    {
                        HostName = _rabbitOptions.HostName,
                        Port = _rabbitOptions.Port,
                        UserName = _rabbitOptions.UserName,
                        Password = _rabbitOptions.Password,
                        VirtualHost = _rabbitOptions.VirtualHost,
                        ClientProvidedName = _rabbitOptions.ClientProvidedName
                    };

                    _rabbitMQConnection = await factory.CreateConnectionAsync(cancellationToken);

                    _consumerChannel = await _rabbitMQConnection.CreateChannelAsync();

                    _consumerChannel.CallbackExceptionAsync += (sender, ea) =>
                    {
                        logger.LogWarning(ea.Exception, "Error with RabbitMQ consumer channel");
                        return Task.CompletedTask;
                    };

                    await _consumerChannel.ExchangeDeclareAsync(
                        exchange: ExchangeName,
                        type: "direct");

                    await _consumerChannel.QueueDeclareAsync(
                        queue: _queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    if (logger.IsEnabled(LogLevel.Trace))
                    {
                        logger.LogTrace("Starting RabbitMQ basic consume");
                    }

                    var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                    consumer.ReceivedAsync += OnMessageReceived;

                    await _consumerChannel.BasicConsumeAsync(
                        queue: _queueName,
                        autoAck: false,
                        consumer: consumer);

                    foreach (var (eventName, _) in _subscriptionInfo.EventTypes)
                    {
                        await _consumerChannel.QueueBindAsync(
                            queue: _queueName,
                            exchange: ExchangeName,
                            routingKey: eventName);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error starting RabbitMQ connection");
                }
            },
            TaskCreationOptions.LongRunning);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


    }
}
