using Customer.Application.IntegrationEvents;
using Customer.Application.IntegrationEvents.Events;
using Customer.Domain.Events;
using MediatR;

namespace Customer.Application.DomainEventHandlers
{
    public class BankCustomerDeletedEventHandler : INotificationHandler<BankCustomerDeletedEvent>
    {
        private readonly ICustomerIntegrationEventService _accountIntegrationEventService;

        public BankCustomerDeletedEventHandler(ICustomerIntegrationEventService accountIntegrationEventService)
        {
            _accountIntegrationEventService = accountIntegrationEventService;
        }

        public async Task Handle(BankCustomerDeletedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new BankCustomerDeletedIntegrationEvent(
                notification.UserId,
                new DTOs.BankCustomerResponseDto
                {
                    FirstName = notification.Customer.FirstName,
                    LastName = notification.Customer.LastName,
                    CreatedAt = notification.Customer.CreatedAt,
                    NationalCode = notification.Customer.NationalCode,
                    Id = notification.Customer.Id
                });

            await _accountIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
