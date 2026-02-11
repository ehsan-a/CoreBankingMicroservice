using Customer.Domain.Aggregates.BankCustomerAggregate;
using MediatR;

namespace Customer.Domain.Events
{
    public class BankCustomerDeletedEvent : INotification
    {
        public BankCustomer Customer { get; }
        public Guid UserId { get; }

        public BankCustomerDeletedEvent(BankCustomer customer, Guid userId)
        {
            Customer = customer;
            UserId = userId;
        }
    }
}
