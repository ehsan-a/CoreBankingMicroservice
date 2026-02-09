using Account.Application.Commands;
using Account.Application.DTOs;
using Account.Application.Interfaces;
using Account.Domain.Aggregates.BankAccountAggregate;
using AutoMapper;
using Shared.Application.Interfaces;

namespace Account.Application.Handlers
{
    public class CreateBankAccountCommandHandler : ICommandHandler<CreateBankAccountCommand, BankAccountResponseDto>
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

        public async Task<BankAccountResponseDto> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            //var customerExists = await _customerRepository.ExistsByIdAsync(request.CustomerId, cancellationToken);

            //if (!customerExists)
            //{
            //    throw new NotFoundException("Customer", request.CustomerId);
            //}
            var accountNumber = await _numberGenerator.GenerateAccountNumberAsync();
            var account = BankAccount.Create(accountNumber, request.CustomerId, request.UserId);
            await _bankAccountRepository.AddAsync(account, cancellationToken);

            await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return _mapper.Map<BankAccountResponseDto>(account);

        }
    }
}
