using Account.Application.DTOs;
using Account.Application.IntegrationEvents;
using Account.Application.IntegrationEvents.Events;
using Account.Domain.Aggregates.BankAccountAggregate;
using Account.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.DomainEventHandlers
{
    public class BankAccountEventHandler : INotificationHandler<BankAccountCreatedEvent>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IAccountIntegrationEventService _accountIntegrationEventService;

        public BankAccountEventHandler(IBankAccountRepository bankAccountRepository,
                                       IAccountIntegrationEventService accountIntegrationEventService
                                       )
        {
            _bankAccountRepository = bankAccountRepository;
            _accountIntegrationEventService = accountIntegrationEventService;

        }

        public async Task Handle(BankAccountCreatedEvent domainEvent, CancellationToken cancellationToken)
        {

            var integrationEvent = new AccountCreatedIntegrationEvent(domainEvent.UserId,
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
