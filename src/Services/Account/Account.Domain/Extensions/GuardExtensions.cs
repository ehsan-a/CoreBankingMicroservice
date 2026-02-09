using Account.Domain.Aggregates.BankAccountAggregate;
using Ardalis.GuardClauses;
using Shared.Domain.Exceptions;

namespace Account.Domain.Extensions
{
    public static class AccountGuards
    {
        public static void Debit(this IGuardClause guardClause, BankAccount bankAccount, decimal amount)
        {
            if (bankAccount.Status != BankAccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");

            if (bankAccount.Balance < amount)
                throw new DomainException("Insufficient funds");
        }

        public static void Credit(this IGuardClause guardClause, BankAccount bankAccount, decimal amount)
        {
            if (bankAccount.Status != BankAccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");
        }
    }
}
