using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Events
{
    public class AccountDeletedEvent : INotification
    {
        public Aggregates.AccountAggregate.Account Account { get; }
        public Guid UserId { get; }

        public AccountDeletedEvent(Aggregates.AccountAggregate.Account account, Guid userId)
        {
            Account = account;
            UserId = userId;
        }
    }
}
