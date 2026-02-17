using Audit.Application.IntegrationEvents.Events;
using Audit.Application.Interfaces;
using Audit.Domain.Aggregates.AuditLogAggregate;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;

namespace Audit.Application.IntegrationEvents.EventHandling
{
    public class ComplianceCreatedIntegrationEventHandler
      (
    IMapper _mapper,
    IAuditLogService _auditLogService,
    ILogger<ComplianceCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<ComplianceCreatedIntegrationEvent>
    {
        public async Task Handle(ComplianceCreatedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var auditLog = _mapper.Map<AuditLog>(@event.Dto);
            auditLog.ActionType = AuditActionType.Create;
            auditLog.PerformedBy = @event.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
