using Shared.EventBus.Events;

namespace Audit.Application.IntegrationEvents.Events
{
    public record BankAccountUpdatedIntegrationEvent : IntegrationEvent
    {
        public BankAccountUpdatedIntegrationEvent(
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
