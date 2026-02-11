using Customer.Domain.Aggregates.BankCustomerAggregate;
using MediatR;

namespace Customer.Domain.Events
{
    public class BankCustomerCreatedEvent : INotification
    {
        public BankCustomer Customer { get; }
        public Guid UserId { get; }

        public BankCustomerCreatedEvent(BankCustomer customer, Guid userId)
        {
            Customer = customer;
            UserId = userId;
        }
    }
}