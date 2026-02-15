using Account.Application.IntegrationEvents.Events;
using Account.Application.Specifications;
using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.EventBus.Abstractions;

namespace Account.Application.IntegrationEvents.EventHandling
{
    public class TransactionCreatedIntegrationEventHandler : IIntegrationEventHandler<TransactionCreatedIntegrationEvent>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public TransactionCreatedIntegrationEventHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task Handle(TransactionCreatedIntegrationEvent @event)
        {
            var spec = new BankAccountGetAllSpec();
            var from = await _bankAccountRepository.GetByIdAsync(@event.TransactionResponseDto.DebitAccountId, spec);
            var to = await _bankAccountRepository.GetByIdAsync(@event.TransactionResponseDto.CreditAccountId, spec);

            from.Debit(@event.TransactionResponseDto.Amount);
            to.Credit(@event.TransactionResponseDto.Amount);

            await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
