using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Application.IntegrationEvents.Events
{
    public record BankCustomerUpdatedIntegrationEvent : IntegrationEvent
    {
        public BankCustomerUpdatedIntegrationEvent(
           Guid userId,
           BankCustomerResponseDto bankCustomerResponseDto,
           string oldValue)
        {
            UserId = userId;
            BankCustomerResponseDto = bankCustomerResponseDto;
            OldValue = oldValue;
        }

        public Guid UserId { get; init; }
        public BankCustomerResponseDto BankCustomerResponseDto { get; init; }
        public string OldValue { get; init; }
    }
}
