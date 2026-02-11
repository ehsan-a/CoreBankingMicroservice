using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Application.IntegrationEvents.Events
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

    public class BankAccountResponseDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;

        public decimal Balance { get; set; }

        public BankAccountStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CustomerId { get; set; }
    }

    public enum BankAccountStatus
    {
        Active = 1,
        Blocked = 2,
        Closed = 3
    }
}
