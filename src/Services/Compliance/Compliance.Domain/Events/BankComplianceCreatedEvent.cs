using Compliance.Domain.Aggregates.BankComplinceAggregate;
using MediatR;

namespace Compliance.Domain.Events
{
    public class BankComplianceCreatedEvent : INotification
    {
        public BankCompliance BankCompliance { get; }
        public Guid UserId { get; }

        public BankComplianceCreatedEvent(BankCompliance bankCompliance, Guid userId)
        {
            BankCompliance = bankCompliance;
            UserId = userId;
        }
    }
}