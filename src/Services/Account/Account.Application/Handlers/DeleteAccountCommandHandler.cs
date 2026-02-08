using Account.Application.Commands;
using Account.Application.Specifications;
using Account.Domain.Aggregates.AccountAggregate;
using Shared.Application.Interfaces;

namespace Account.Application.Handlers
{
    public class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var item = await _accountRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            if (item == null) return;

            Domain.Aggregates.AccountAggregate.Account.Delete(item, request.UserId);

            _accountRepository.Delete(item);
            await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
