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
        public BankCustomerStatus Status { get; private set; } = BankCustomerStatus.Active;

        public static BankCustomer Create(string nationalCode, string firstName, string lastName, Guid userId)
        {
            var customer = new BankCustomer(nationalCode, firstName, lastName);
            customer.AddDomainEvent(new BankCustomerCreatedEvent(customer, userId));
            return customer;
        }
        public void Delete(Guid userId)
        {
            Guard.Against.NullOrEmpty(userId, nameof(userId));
            IsDeleted = true;

            AddDomainEvent(new BankCustomerDeletedEvent(this, userId));
        }

        public void ChangeFullName(string firstName, string lastName, string oldValue, Guid userId)
        {
            Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            Guard.Against.NullOrEmpty(lastName, nameof(lastName));

            FirstName = firstName;
            LastName = lastName;

            //AddDomainEvent(new BankCustomerUpdatedEvent(this, userId, oldValue));
        }
        public void ChangeStatus(BankCustomerStatus status, string oldValue, Guid userId)
        {
            Guard.Against.EnumOutOfRange(status, nameof(status));

            Status = status;

            AddDomainEvent(new BankCustomerUpdatedEvent(this, userId, oldValue));
        }
    }
}
