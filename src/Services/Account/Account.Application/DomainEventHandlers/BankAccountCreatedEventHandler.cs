using Account.Application.DTOs;
using Account.Application.IntegrationEvents;
using Account.Application.IntegrationEvents.Events;
using Account.Domain.Events;
using MediatR;

namespace Account.Application.DomainEventHandlers
{
    public class BankAccountCreatedEventHandler : INotificationHandler<BankAccountCreatedEvent>
    {
        private readonly IAccountIntegrationEventService _accountIntegrationEventService;

        public BankAccountCreatedEventHandler(IAccountIntegrationEventService accountIntegrationEventService)
        {
            _accountIntegrationEventService = accountIntegrationEventService;

        }

        public async Task Handle(BankAccountCreatedEvent domainEvent, CancellationToken cancellationToken)
        {

            var integrationEvent = new BankAccountCreatedIntegrationEvent(domainEvent.UserId,
                new BankAccountResponseDto
                {
                    Id = domainEvent.BankAccount.Id,
                    AccountNumber = domainEvent.BankAccount.AccountNumber,
                    Balance = domainEvent.BankAccount.Balance,
                    CreatedAt = domainEvent.BankAccount.CreatedAt,
                    CustomerId = domainEvent.BankAccount.CustomerId,
                    Status = domainEvent.BankAccount.Status
                }
               );
            await _accountIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
