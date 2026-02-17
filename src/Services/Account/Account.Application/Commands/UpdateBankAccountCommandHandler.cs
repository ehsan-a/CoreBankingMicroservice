using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;
using System.Text.Json;

namespace Account.Application.Commands
{
    public class UpdateBankAccountCommandHandler : ICommandHandler<UpdateBankAccountCommand>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public UpdateBankAccountCommandHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _bankAccountRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Account Not Found!");

            var oldAccount = await _bankAccountRepository
              .GetByIdAsNoTrackingAsync(account.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            account.ChangeStatus(request.Status, oldValue, request.UserId);

            _bankAccountRepository.Update(account);

            await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
