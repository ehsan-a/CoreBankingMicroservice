using Shared.EventBus.Events;

namespace Audit.Application.IntegrationEvents.Events
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
    public class RegisteredComplianceResponseDto
    {
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
