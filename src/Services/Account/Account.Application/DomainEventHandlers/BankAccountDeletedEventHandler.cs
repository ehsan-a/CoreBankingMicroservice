using Account.Application.IntegrationEvents;
using Account.Application.IntegrationEvents.Events;
using Account.Domain.Events;
using MediatR;

namespace Account.Application.DomainEventHandlers
{
    public class BankAccountDeletedEventHandler : INotificationHandler<BankAccountDeletedEvent>
    {
        private readonly IAccountIntegrationEventService _accountIntegrationEventService;

        public BankAccountDeletedEventHandler(IAccountIntegrationEventService accountIntegrationEventService)
        {
            _accountIntegrationEventService = accountIntegrationEventService;
        }

        public async Task Handle(BankAccountDeletedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new BankAccountDeletedIntegrationEvent
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
                }
                );

            await _accountIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
