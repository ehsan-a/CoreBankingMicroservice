using Account.Domain.Aggregates.BankAccountAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Events
{
    public class BankAccountUpdatedEvent : INotification
    {
        public BankAccount BankAccount { get; }
        public string? OldValue { get; }
        public Guid UserId { get; }

        public BankAccountUpdatedEvent(BankAccount bankAccount, Guid userId, string? oldValue)
        {
            BankAccount = bankAccount;
            UserId = userId;
            OldValue = oldValue;
        }
    }
}
