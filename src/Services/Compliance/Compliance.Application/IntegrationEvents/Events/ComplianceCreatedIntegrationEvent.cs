using Compliance.Application.DTOs;
using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Shared.EventBus.Events;

namespace Compliance.Application.IntegrationEvents.Events
{
    public record ComplianceCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public RegisteredComplianceResponseDto Dto { get; init; }
        public ComplianceCreatedIntegrationEvent(Guid userId, RegisteredComplianceResponseDto dto)
        {
            UserId = userId;
            Dto = dto;
        }
    }
}
