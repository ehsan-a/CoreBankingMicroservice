using Compliance.Application.IntegrationEvents;
using Compliance.Application.IntegrationEvents.Events;
using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Compliance.Domain.Events;
using MediatR;

namespace Compliance.Application.DomainEventHandlers
{
    public class ComplianceCreatedEventHandler : INotificationHandler<BankComplianceCreatedEvent>
    {
        private readonly IComplianceIntegrationEventService _complianceIntegrationEventService;

        public ComplianceCreatedEventHandler(IComplianceIntegrationEventService complianceIntegrationEventService)
        {
            _complianceIntegrationEventService = complianceIntegrationEventService;
        }

        public async Task Handle(BankComplianceCreatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new ComplianceCreatedIntegrationEvent(notification.UserId,
                new DTOs.RegisteredComplianceResponseDto
                {
                    CentralBankCreditCheckPassed = notification.BankCompliance.CentralBankCreditCheckPassed,
                    CivilRegistryVerified = notification.BankCompliance.CivilRegistryVerified,
                    PoliceClearancePassed = notification.BankCompliance.PoliceClearancePassed,
                    CreatedAt = notification.BankCompliance.CreatedAt
                });
            await _complianceIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
