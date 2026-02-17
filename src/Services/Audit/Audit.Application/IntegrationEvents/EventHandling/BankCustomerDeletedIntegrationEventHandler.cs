using Audit.Application.IntegrationEvents.Events;
using Audit.Application.Interfaces;
using Audit.Domain.Aggregates.AuditLogAggregate;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;

namespace Audit.Application.IntegrationEvents.EventHandling
{
    public class BankCustomerDeletedIntegrationEventHandler
    (
    IMapper _mapper,
    IAuditLogService _auditLogService,
    ILogger<BankCustomerDeletedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<BankCustomerDeletedIntegrationEvent>
    {
        public async Task Handle(BankCustomerDeletedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var auditLog = _mapper.Map<AuditLog>(@event.BankCustomerResponseDto);
            auditLog.ActionType = AuditActionType.Delete;
            auditLog.PerformedBy = @event.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
