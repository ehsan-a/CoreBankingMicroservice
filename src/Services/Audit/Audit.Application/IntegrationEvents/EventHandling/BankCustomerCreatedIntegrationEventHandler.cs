using Audit.Application.IntegrationEvents.Events;
using Audit.Application.Interfaces;
using Audit.Domain.Aggregates.AuditLogAggregate;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Application.IntegrationEvents.EventHandling
{
    public class BankCustomerCreatedIntegrationEventHandler
    (
    IMapper _mapper,
    IAuditLogService _auditLogService,
    ILogger<BankCustomerCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<BankCustomerCreatedIntegrationEvent>
    {
        public async Task Handle(BankCustomerCreatedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var auditLog = _mapper.Map<AuditLog>(@event.BankCustomerResponseDto);
            auditLog.ActionType = AuditActionType.Create;
            auditLog.PerformedBy = @event.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
