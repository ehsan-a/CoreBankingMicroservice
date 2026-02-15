using Shared.EventBus.Events;

namespace Account.Application.IntegrationEvents.Events
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
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public Guid DebitAccountId { get; set; }
        public Guid CreditAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
    }
    public enum TransactionType
    {
        Transfer, Deposit, Withdrawal
    }
}
