using Account.Domain.Aggregates.AccountAggregate;
using Ardalis.GuardClauses;
using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Extensions
{
    public static class AccountGuards
    {
        public static void Debit(this IGuardClause guardClause, Aggregates.AccountAggregate.Account account, decimal amount)
        {
            if (account.Status != AccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");

            if (account.Balance < amount)
                throw new DomainException("Insufficient funds");
        }

        public static void Credit(this IGuardClause guardClause, Aggregates.AccountAggregate.Account account, decimal amount)
        {
            if (account.Status != AccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");
        }
    }
}
