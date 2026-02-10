using Account.Application.Specifications;
using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.Application.Interfaces;

namespace Account.Application.Commands
{
    public class DeleteBankAccountCommandHandler : ICommandHandler<DeleteBankAccountCommand>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public DeleteBankAccountCommandHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var spec = new BankAccountGetAllSpec();
            var item = await _bankAccountRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            if (item == null) return;

            BankAccount.Delete(item, request.UserId);

            _bankAccountRepository.Delete(item);
            await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
