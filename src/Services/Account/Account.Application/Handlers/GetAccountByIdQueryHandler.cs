using Account.Application.DTOs;
using Account.Application.Queries;
using Account.Application.Specifications;
using Account.Domain.Aggregates.AccountAggregate;
using AutoMapper;
using Shared.Application.Interfaces;

namespace Account.Application.Handlers
{
    public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountResponseDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountResponseDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var account = await _accountRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<AccountResponseDto>(account);
        }
    }
}
