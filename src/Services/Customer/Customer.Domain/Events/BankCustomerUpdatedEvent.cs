using Customer.Domain.Aggregates.BankCustomerAggregate;
using MediatR;

namespace Customer.Domain.Events
{
    public class BankCustomerUpdatedEvent : INotification
    {
        public BankCustomer Customer { get; }
        public string? OldValue { get; }
        public Guid UserId { get; }

        public BankCustomerUpdatedEvent(BankCustomer customer, Guid userId, string? oldValue)
        {
            Customer = customer;
            UserId = userId;
            OldValue = oldValue;
        }
    }
}