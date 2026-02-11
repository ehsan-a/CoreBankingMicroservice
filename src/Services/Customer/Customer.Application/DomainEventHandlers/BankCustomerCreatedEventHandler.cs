using Customer.Application.IntegrationEvents;
using Customer.Application.IntegrationEvents.Events;
using Customer.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.DomainEventHandlers
{
    public class BankCustomerCreatedEventHandler : INotificationHandler<BankCustomerCreatedEvent>
    {
        private readonly ICustomerIntegrationEventService _accountIntegrationEventService;

        public BankCustomerCreatedEventHandler(ICustomerIntegrationEventService accountIntegrationEventService)
        {
            _accountIntegrationEventService = accountIntegrationEventService;
        }

        public async Task Handle(BankCustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new BankCustomerCreatedIntegrationEvent(notification.UserId,
                new DTOs.BankCustomerResponseDto
                {
                    CreatedAt = notification.Customer.CreatedAt,
                    FirstName = notification.Customer.FirstName,
                    LastName = notification.Customer.LastName,
                    Id = notification.Customer.Id,
                    NationalCode = notification.Customer.NationalCode
                });
            await _accountIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
