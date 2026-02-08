using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Events
{
    public class AccountUpdatedEvent : INotification
    {
        public Aggregates.AccountAggregate.Account Account { get; }
        public string? OldValue { get; }
        public Guid UserId { get; }

        public AccountUpdatedEvent(Aggregates.AccountAggregate.Account account, Guid userId, string? oldValue)
        {
            Account = account;
            UserId = userId;
            OldValue = oldValue;
        }
    }
}
