using Account.Application.Commands;
using Account.Application.DTOs;
using Account.Application.Interfaces;
using Account.Domain.Aggregates.BankAccountAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Application.Interfaces;

namespace Account.Application.Handlers
{
    public class CreateBankAccountCommandHandler : ICommandHandler<CreateBankAccountCommand, bool>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;
        private readonly INumberGenerator _numberGenerator;

        public CreateBankAccountCommandHandler(IBankAccountRepository bankAccountRepository, IMapper mapper, INumberGenerator numberGenerator)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
            _numberGenerator = numberGenerator;
        }

        public async Task<bool> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            //var customerExists = await _customerRepository.ExistsByIdAsync(request.CustomerId, cancellationToken);

            //if (!customerExists)
            //{
            //    throw new NotFoundException("Customer", request.CustomerId);
            //}
            var accountNumber = await _numberGenerator.GenerateAccountNumberAsync();
            var account = BankAccount.Create(accountNumber, request.CustomerId, request.UserId);
            await _bankAccountRepository.AddAsync(account, cancellationToken);

            return await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }

    // Use for Idempotency in Command process
    public class CreateBankAccountIdentifiedCommandHandler : IdentifiedCommandHandler<CreateBankAccountCommand, bool>
    {
        public CreateBankAccountIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CreateBankAccountCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true; // Ignore duplicate requests for processing order.
        }
    }
}
