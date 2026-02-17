using Audit.Application.IntegrationEvents.Events;
using Audit.Application.Interfaces;
using Audit.Domain.Aggregates.AuditLogAggregate;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;

namespace Audit.Application.IntegrationEvents.EventHandling
{
    public class TransactionCreatedIntegrationEventHandler
    (
    IMapper _mapper,
    IAuditLogService _auditLogService,
    ILogger<TransactionCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<TransactionCreatedIntegrationEvent>
    {
        public async Task Handle(TransactionCreatedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var auditLog = _mapper.Map<AuditLog>(@event.TransactionResponseDto);
            auditLog.ActionType = AuditActionType.Create;
            auditLog.PerformedBy = @event.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
