using Customer.Application.DTOs;
using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.IntegrationEvents.Events
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
}
