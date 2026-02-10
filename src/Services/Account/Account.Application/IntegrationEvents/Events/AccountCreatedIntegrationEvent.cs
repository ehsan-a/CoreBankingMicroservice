using Account.Application.DTOs;
using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.IntegrationEvents.Events
{
    public record AccountCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public BankAccountResponseDto BankAccountResponseDto { get; init; }
        public AccountCreatedIntegrationEvent(Guid userId, BankAccountResponseDto bankAccountResponseDto)
        {
            UserId = userId;
            BankAccountResponseDto = bankAccountResponseDto;
        }
    }
}
