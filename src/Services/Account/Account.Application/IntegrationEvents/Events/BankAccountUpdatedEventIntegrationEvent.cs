using Account.Application.DTOs;
using Shared.EventBus.Events;

namespace Account.Application.IntegrationEvents.Events
{
    public record BankAccountUpdatedEventIntegrationEvent : IntegrationEvent
    {
        public BankAccountUpdatedEventIntegrationEvent(
            Guid userId,
            BankAccountResponseDto bankAccountResponseDto,
            string oldValue)
        {
            UserId = userId;
            BankAccountResponseDto = bankAccountResponseDto;
            OldValue = oldValue;
        }

        public Guid UserId { get; init; }
        public BankAccountResponseDto BankAccountResponseDto { get; init; }
        public string OldValue { get; init; }
    }
}
