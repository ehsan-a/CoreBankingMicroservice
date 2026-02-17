using Audit.Application.IntegrationEvents.Events;
using Audit.Application.Interfaces;
using Audit.Domain.Aggregates.AuditLogAggregate;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;

namespace Audit.Application.IntegrationEvents.EventHandling
{
    public class BankCustomerUpdatedIntegrationEventHandler
      (
    IMapper _mapper,
    IAuditLogService _auditLogService,
    ILogger<BankCustomerUpdatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<BankCustomerUpdatedIntegrationEvent>
    {
        public async Task Handle(BankCustomerUpdatedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var auditLog = _mapper.Map<AuditLog>(@event.BankCustomerResponseDto);
            auditLog.ActionType = AuditActionType.Update;
            auditLog.PerformedBy = @event.UserId.ToString();
            auditLog.OldValue = @event.OldValue;
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
