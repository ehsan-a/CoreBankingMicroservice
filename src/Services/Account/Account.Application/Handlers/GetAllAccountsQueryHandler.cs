
using Account.Application.DTOs;
using Account.Application.Queries;
using Account.Application.Specifications;
using Account.Domain.Aggregates.AccountAggregate;
using AutoMapper;
using Shared.Application.Interfaces;

namespace Account.Application.Handlers
{
    public class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountResponseDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountResponseDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec(request.Limit, request.Offset);
            var accounts = await _accountRepository.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<AccountResponseDto>>(accounts);
        }
    }
}
