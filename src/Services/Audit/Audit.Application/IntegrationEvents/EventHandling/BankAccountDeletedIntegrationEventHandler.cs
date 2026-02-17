using Audit.Application.IntegrationEvents.Events;
using Audit.Application.Interfaces;
using Audit.Domain.Aggregates.AuditLogAggregate;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;

namespace Audit.Application.IntegrationEvents.EventHandling
{
    public class BankAccountDeletedIntegrationEventHandler
    (
    IMapper _mapper,
    IAuditLogService _auditLogService,
    ILogger<BankAccountDeletedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<BankAccountDeletedIntegrationEvent>
    {
        public async Task Handle(BankAccountDeletedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var auditLog = _mapper.Map<AuditLog>(@event.BankAccountResponseDto);
            auditLog.ActionType = AuditActionType.Delete;
            auditLog.PerformedBy = @event.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
