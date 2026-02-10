using Account.Domain.Aggregates.BankAccountAggregate;
using AutoMapper;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;
using System.Text.Json;

namespace Account.Application.Commands
{
    public class UpdateBankAccountCommandHandler : ICommandHandler<UpdateBankAccountCommand>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public UpdateBankAccountCommandHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _bankAccountRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
            if (account == null) throw new NotFoundException("");

            _mapper.Map(request, account);

            var oldAccount = await _bankAccountRepository
              .GetByIdAsNoTrackingAsync(account.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _bankAccountRepository.Update(account);

            BankAccount.Update(account, request.UserId, oldValue);

            await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
