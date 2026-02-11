using Ardalis.GuardClauses;
using Customer.Domain.Events;
using Shared.Domain.Abstractions;
using Shared.Domain.Interfaces;

namespace Customer.Domain.Aggregates.BankCustomerAggregate
{
    public class BankCustomer : BaseEntity, ISoftDeletable, IAggregateRoot
    {
        private BankCustomer(string nationalCode, string firstName, string lastName)
        {
            Guard.Against.NullOrEmpty(nationalCode, nameof(nationalCode));
            Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            Guard.Against.NullOrEmpty(lastName, nameof(lastName));

            NationalCode = nationalCode;
            FirstName = firstName;
            LastName = lastName;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private BankCustomer() { }

        public string NationalCode { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public static BankCustomer Create(string nationalCode, string firstName, string lastName, Guid userId)
        {
            var customer = new BankCustomer(nationalCode, firstName, lastName);
            customer.AddDomainEvent(new BankCustomerCreatedEvent(customer, userId));
            return customer;
        }

        public static BankCustomer Delete(BankCustomer customer, Guid userId)
        {
            customer.AddDomainEvent(
                new BankCustomerDeletedEvent(customer, userId)
            );
            return customer;
        }

        public static BankCustomer Update(BankCustomer customer, Guid userId, string oldValue)
        {
            customer.AddDomainEvent(
                new BankCustomerUpdatedEvent(customer, userId, oldValue)
            );
            return customer;
        }
    }
}
