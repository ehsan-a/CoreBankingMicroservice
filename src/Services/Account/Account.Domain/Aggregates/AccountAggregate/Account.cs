using Account.Domain.Events;
using Account.Domain.Extensions;
using Ardalis.GuardClauses;
using Shared.Domain.Abstracttion;
using Shared.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Account.Domain.Aggregates.AccountAggregate
{
    public class Account : BaseEntity, ISoftDeletable, IAggregateRoot
    {
        private Account(string accountNumber, Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            Guard.Against.NullOrEmpty(accountNumber, nameof(accountNumber));

            AccountNumber = accountNumber;
            CustomerId = customerId;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private Account() { }

        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; } = 0;
        public AccountStatus Status { get; private set; } = AccountStatus.Active;
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

        public void ChangeStatus(AccountStatus status)
        {
            Guard.Against.EnumOutOfRange(status, nameof(status));
            Status = status;
        }

        public static Account Create(string accountNumber, Guid customerId, Guid userId)
        {
            var account = new Account(accountNumber, customerId);
            account.AddDomainEvent(new AccountCreatedEvent(account, userId));
            return account;
        }

        public static Account Delete(Account account, Guid userId)
        {
            account.AddDomainEvent(
                new AccountDeletedEvent(account, userId)
            );
            return account;
        }

        public static Account Update(Account account, Guid userId, string oldValue)
        {
            account.AddDomainEvent(
                new AccountUpdatedEvent(account, userId, oldValue)
            );
            return account;
        }
    }
}
