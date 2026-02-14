using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Application.DTOs
{
    public class CreateTransactionRequestDto
    {
        public Guid DebitAccountId { get; set; }
        public Guid CreditAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
    }
}
