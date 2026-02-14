using MediatR;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Domain.Events
{
    public class TransactionCreatedEvent : INotification
    {
        public AccountTransaction AccountTransaction { get; }
        public Guid UserId { get; }

        public TransactionCreatedEvent(AccountTransaction accountTransaction, Guid userId)
        {
            AccountTransaction = accountTransaction;
            UserId = userId;
        }
    }

}