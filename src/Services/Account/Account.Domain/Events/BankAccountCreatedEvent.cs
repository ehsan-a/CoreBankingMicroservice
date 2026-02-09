using Account.Domain.Aggregates.BankAccountAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Events
{
    public class BankAccountCreatedEvent : INotification
    {
        public BankAccount BankAccount { get; }
        public Guid UserId { get; }

        public BankAccountCreatedEvent(BankAccount bankAccount, Guid userId)
        {
            BankAccount = bankAccount;
            UserId = userId;
        }
    }
}
