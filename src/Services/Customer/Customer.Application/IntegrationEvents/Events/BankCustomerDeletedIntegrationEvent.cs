using Customer.Application.DTOs;
using Shared.EventBus.Events;

namespace Customer.Application.IntegrationEvents.Events
{
    public record BankCustomerDeletedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; init; }
        public BankCustomerResponseDto BankCustomerResponseDto { get; init; }
        public BankCustomerDeletedIntegrationEvent(Guid userId, BankCustomerResponseDto bankCustomerResponseDto)
        {
            UserId = userId;
            BankCustomerResponseDto = bankCustomerResponseDto;
        }
    }
}
