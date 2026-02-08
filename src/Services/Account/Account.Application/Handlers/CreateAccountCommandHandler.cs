using Account.Application.Commands;
using Account.Application.DTOs;
using Account.Application.Interfaces;
using Account.Domain.Aggregates.AccountAggregate;
using AutoMapper;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Handlers
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountResponseDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly INumberGenerator _numberGenerator;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper, INumberGenerator numberGenerator)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _numberGenerator = numberGenerator;
        }

        public async Task<AccountResponseDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            //var customerExists = await _customerRepository.ExistsByIdAsync(request.CustomerId, cancellationToken);

            //if (!customerExists)
            //{
            //    throw new NotFoundException("Customer", request.CustomerId);
            //}
            var accountNumber = await _numberGenerator.GenerateAccountNumberAsync();
            var account = Domain.Aggregates.AccountAggregate.Account.Create(accountNumber, request.CustomerId, request.UserId);
            await _accountRepository.AddAsync(account, cancellationToken);

            await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return _mapper.Map<AccountResponseDto>(account);

        }
    }
}
