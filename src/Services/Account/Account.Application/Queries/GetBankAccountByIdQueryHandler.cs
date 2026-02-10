using Account.Application.DTOs;
using Account.Application.Specifications;
using Account.Domain.Aggregates.BankAccountAggregate;
using AutoMapper;
using Shared.Application.Interfaces;

namespace Account.Application.Queries
{
    public class GetBankAccountByIdQueryHandler : IQueryHandler<GetBankAccountByIdQuery, BankAccountResponseDto>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetBankAccountByIdQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<BankAccountResponseDto> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new BankAccountGetAllSpec();
            var account = await _bankAccountRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<BankAccountResponseDto>(account);
        }
    }
}
