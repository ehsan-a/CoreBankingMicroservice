using Account.Application.DTOs;
using Account.Application.Specifications;
using Account.Domain.Aggregates.BankAccountAggregate;
using AutoMapper;
using Shared.Application.Interfaces;

namespace Account.Application.Queries
{
    public class GetAllBankAccountsQueryHandler : IQueryHandler<GetAllBankAccountsQuery, IEnumerable<BankAccountResponseDto>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetAllBankAccountsQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankAccountResponseDto>> Handle(GetAllBankAccountsQuery request, CancellationToken cancellationToken)
        {
            var spec = new BankAccountGetAllSpec(request.Limit, request.Offset);
            var accounts = await _bankAccountRepository.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<BankAccountResponseDto>>(accounts);
        }
    }
}
