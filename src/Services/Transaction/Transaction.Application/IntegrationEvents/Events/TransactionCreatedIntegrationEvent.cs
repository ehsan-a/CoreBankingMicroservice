using Shared.EventBus.Events;
using Transaction.Application.DTOs;

namespace Transaction.Application.IntegrationEvents.Events
{
    public record TransactionCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public TransactionResponseDto TransactionResponseDto { get; init; }
        public TransactionCreatedIntegrationEvent(Guid userId, TransactionResponseDto transactionResponseDto)
        {
            UserId = userId;
            TransactionResponseDto = transactionResponseDto;
        }
    }
}
