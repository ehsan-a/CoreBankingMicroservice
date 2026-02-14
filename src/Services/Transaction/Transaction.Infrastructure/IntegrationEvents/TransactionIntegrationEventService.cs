using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;
using Shared.EventBus.Events;
using Shared.IntegrationEventLogEF.Services;
using Transaction.Application.IntegrationEvents;
using Transaction.Infrastructure.Persistence;

namespace Transaction.Infrastructure.IntegrationEvents
{
    public class TransactionIntegrationEventService(IEventBus eventBus,
    TransactionDbContext transactionDbContext,
    IIntegrationEventLogService integrationEventLogService,
    ILogger<TransactionIntegrationEventService> logger) : ITransactionIntegrationEventService
    {
        private readonly IEventBus _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        private readonly TransactionDbContext _transactionDbContext = transactionDbContext ?? throw new ArgumentNullException(nameof(transactionDbContext));
        private readonly IIntegrationEventLogService _eventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
        private readonly ILogger<TransactionIntegrationEventService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    await _eventBus.PublishAsync(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", logEvt.EventId);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, _transactionDbContext.GetCurrentTransaction());
        }
    }

}
