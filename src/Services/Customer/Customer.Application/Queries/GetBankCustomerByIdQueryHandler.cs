using AutoMapper;
using Customer.Application.DTOs;
using Customer.Application.Specifications;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetBankCustomerByIdQueryHandler : IQueryHandler<GetBankCustomerByIdQuery, BankCustomerResponseDto>
    {
        private readonly IBankCustomerRepository _bankCustomerRepository;
        private readonly IMapper _mapper;

        public GetBankCustomerByIdQueryHandler(IBankCustomerRepository bankCustomerRepository, IMapper mapper)
        {
            _bankCustomerRepository = bankCustomerRepository;
            _mapper = mapper;
        }

        public async Task<BankCustomerResponseDto> Handle(GetBankCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new BankCustomerGetAllSpec();
            var customer = await _bankCustomerRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<BankCustomerResponseDto>(customer);
        }
    }
}
