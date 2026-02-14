using Compliance.Application.IntegrationEvents;
using Compliance.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;
using Shared.EventBus.Events;
using Shared.IntegrationEventLogEF.Services;

namespace Compliance.Infrastructure.IntegrationEvents
{
    public class ComplianceIntegrationEventService(IEventBus eventBus,
    ComplianceDbContext complianceDbContext,
    IIntegrationEventLogService integrationEventLogService,
    ILogger<ComplianceIntegrationEventService> logger) : IComplianceIntegrationEventService
    {
        private readonly IEventBus _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        private readonly ComplianceDbContext _complianceDbContext = complianceDbContext ?? throw new ArgumentNullException(nameof(complianceDbContext));
        private readonly IIntegrationEventLogService _eventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
        private readonly ILogger<ComplianceIntegrationEventService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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

            await _eventLogService.SaveEventAsync(evt, _complianceDbContext.GetCurrentTransaction());
        }
    }

}
