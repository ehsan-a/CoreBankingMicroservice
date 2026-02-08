
using Account.Application.Commands;
using Account.Domain.Aggregates.AccountAggregate;
using AutoMapper;
using Shared.Application.Exceptions;
using Shared.Application.Interfaces;
using System.Text.Json;

namespace Account.Application.Handlers
{
    public class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
            if (account == null) throw new NotFoundException("");

            _mapper.Map(request, account);

            var oldAccount = await _accountRepository
              .GetByIdAsNoTrackingAsync(account.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _accountRepository.Update(account);

            Domain.Aggregates.AccountAggregate.Account.Update(account, request.UserId, oldValue);

            await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
