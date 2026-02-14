using Shared.Application.Interfaces;
using Transaction.Application.DTOs;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Application.Commands
{
    public class CreateTransactionCommand : ICommand<TransactionResponseDto>
    {
        public Guid DebitAccountId { get; set; }
        public Guid CreditAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public Guid UserId { get; set; }
    }
}
