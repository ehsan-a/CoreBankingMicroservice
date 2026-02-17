using Shared.EventBus.Events;

namespace Audit.Application.IntegrationEvents.Events
{
    public record BankAccountDeletedIntegrationEvent : IntegrationEvent
    {
        public BankAccountDeletedIntegrationEvent(Guid userId, BankAccountResponseDto bankAccountResponseDto)
        {
            UserId = userId;
            BankAccountResponseDto = bankAccountResponseDto;
        }

        public Guid UserId { get; init; }
        public BankAccountResponseDto BankAccountResponseDto { get; init; }
    }
}
