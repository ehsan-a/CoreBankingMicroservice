using Account.Domain.Events;
using Account.Domain.Extensions;
using Ardalis.GuardClauses;
using Shared.Domain.Abstractions;
using Shared.Domain.Interfaces;

namespace Account.Domain.Aggregates.BankAccountAggregate
{
    public class BankAccount : BaseEntity, ISoftDeletable, IAggregateRoot
    {
        private BankAccount(string accountNumber, Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            Guard.Against.NullOrEmpty(accountNumber, nameof(accountNumber));

            AccountNumber = accountNumber;
            CustomerId = customerId;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private BankAccount() { }

        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; } = 0;
        public BankAccountStatus Status { get; private set; } = BankAccountStatus.Active;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public Guid CustomerId { get; private set; }
        public bool IsDeleted { get; set; } = false;

        public void Debit(decimal amount)
        {
            Guard.Against.Debit(this, amount);

            Balance -= amount;
        }

        public void Credit(decimal amount)
        {
            Guard.Against.Credit(this, amount);

            Balance += amount;
        }

        public void ChangeStatus(BankAccountStatus status)
        {
            Guard.Against.EnumOutOfRange(status, nameof(status));
            Status = status;
        }

        public static BankAccount Create(string accountNumber, Guid customerId, Guid userId)
        {
            var account = new BankAccount(accountNumber, customerId);
            account.AddDomainEvent(new BankAccountCreatedEvent(account, userId));
            return account;
        }

        public static BankAccount Delete(BankAccount account, Guid userId)
        {
            account.AddDomainEvent(
                new BankAccountDeletedEvent(account, userId)
            );
            return account;
        }

        public static BankAccount Update(BankAccount account, Guid userId, string oldValue)
        {
            account.AddDomainEvent(
                new BankAccountUpdatedEvent(account, userId, oldValue)
            );
            return account;
        }
    }
}
