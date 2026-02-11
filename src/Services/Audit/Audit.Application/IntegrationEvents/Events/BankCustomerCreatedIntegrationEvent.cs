using Shared.EventBus.Events;

namespace Audit.Application.IntegrationEvents.Events
{
    public record BankCustomerCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public BankCustomerResponseDto BankCustomerResponseDto { get; init; }
        public BankCustomerCreatedIntegrationEvent(Guid userId, BankCustomerResponseDto bankCustomerResponseDto)
        {
            UserId = userId;
            BankCustomerResponseDto = bankCustomerResponseDto;
        }
    }
    public class BankCustomerResponseDto
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
