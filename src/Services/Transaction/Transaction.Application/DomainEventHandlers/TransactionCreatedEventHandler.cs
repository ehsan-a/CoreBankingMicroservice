using MediatR;
using Transaction.Application.IntegrationEvents;
using Transaction.Application.IntegrationEvents.Events;
using Transaction.Domain.Events;

namespace Transaction.Application.DomainEventHandlers
{
    public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedEvent>
    {
        private readonly ITransactionIntegrationEventService _transactionIntegrationEventService;

        public TransactionCreatedEventHandler(ITransactionIntegrationEventService transactionIntegrationEventService)
        {
            _transactionIntegrationEventService = transactionIntegrationEventService;
        }

        public async Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new TransactionCreatedIntegrationEvent(notification.UserId,
                new DTOs.TransactionResponseDto
                {
                    CreditAccountId = notification.AccountTransaction.CreditAccountId,
                    DebitAccountId = notification.AccountTransaction.DebitAccountId,
                    Amount = notification.AccountTransaction.Amount,
                    CreatedAt = notification.AccountTransaction.CreatedAt,
                    Description = notification.AccountTransaction.Description,
                    Id = notification.AccountTransaction.Id,
                    Type = notification.AccountTransaction.Type
                });
            await _transactionIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
