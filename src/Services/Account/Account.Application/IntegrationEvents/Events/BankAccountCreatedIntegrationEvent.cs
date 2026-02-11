using Account.Application.DTOs;
using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.IntegrationEvents.Events
{
    public record BankAccountCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public BankAccountResponseDto BankAccountResponseDto { get; init; }
        public BankAccountCreatedIntegrationEvent(Guid userId, BankAccountResponseDto bankAccountResponseDto)
        {
            UserId = userId;
            BankAccountResponseDto = bankAccountResponseDto;
        }
    }
}
