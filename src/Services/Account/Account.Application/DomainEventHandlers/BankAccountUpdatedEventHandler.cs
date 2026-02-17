using Account.Application.IntegrationEvents;
using Account.Application.IntegrationEvents.Events;
using Account.Domain.Events;
using MediatR;

namespace Account.Application.DomainEventHandlers
{
    public class BankAccountUpdatedEventHandler : INotificationHandler<BankAccountUpdatedEvent>
    {
        private readonly IAccountIntegrationEventService _accountIntegrationEventService;

        public BankAccountUpdatedEventHandler(IAccountIntegrationEventService accountIntegrationEventService)
        {
            _accountIntegrationEventService = accountIntegrationEventService;
        }

        public async Task Handle(BankAccountUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new BankAccountUpdatedEventIntegrationEvent
            (
                notification.UserId,
                 new DTOs.BankAccountResponseDto
                 {
                     AccountNumber = notification.BankAccount.AccountNumber,
                     Balance = notification.BankAccount.Balance,
                     CreatedAt = notification.BankAccount.CreatedAt,
                     CustomerId = notification.BankAccount.CustomerId,
                     Id = notification.BankAccount.Id,
                     Status = notification.BankAccount.Status
                 },
                notification.OldValue
            );

            await _accountIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
