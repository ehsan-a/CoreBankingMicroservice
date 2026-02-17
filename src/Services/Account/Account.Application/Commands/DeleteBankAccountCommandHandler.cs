using Account.Application.Specifications;
using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.Application.Exceptions;
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
            var item = await _bankAccountRepository.GetByIdAsync(request.Id, spec, cancellationToken)
            ?? throw new NotFoundException("Account Not Found!");

            item.Delete(request.UserId);
            await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
