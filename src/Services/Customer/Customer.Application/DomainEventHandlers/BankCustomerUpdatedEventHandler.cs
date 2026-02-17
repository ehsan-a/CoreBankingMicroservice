using Customer.Application.IntegrationEvents;
using Customer.Application.IntegrationEvents.Events;
using Customer.Domain.Events;
using MediatR;

namespace Customer.Application.DomainEventHandlers
{
    public class BankCustomerUpdatedEventHandler : INotificationHandler<BankCustomerUpdatedEvent>
    {
        private readonly ICustomerIntegrationEventService _accountIntegrationEventService;

        public BankCustomerUpdatedEventHandler(ICustomerIntegrationEventService accountIntegrationEventService)
        {
            _accountIntegrationEventService = accountIntegrationEventService;
        }

        public async Task Handle(BankCustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new BankCustomerUpdatedIntegrationEvent(notification.UserId,
                new DTOs.BankCustomerResponseDto
                {
                    CreatedAt = notification.Customer.CreatedAt,
                    FirstName = notification.Customer.FirstName,
                    LastName = notification.Customer.LastName,
                    Id = notification.Customer.Id,
                    NationalCode = notification.Customer.NationalCode
                },
                notification.OldValue);
            await _accountIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
