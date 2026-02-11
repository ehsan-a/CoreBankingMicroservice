using AutoMapper;
using Customer.Application.DTOs;
using Customer.Application.Specifications;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetAllBankCustomersQueryHandler : IQueryHandler<GetAllBankCustomersQuery, IEnumerable<BankCustomerResponseDto>>
    {
        private readonly IBankCustomerRepository _bankCustomerRepository;
        private readonly IMapper _mapper;

        public GetAllBankCustomersQueryHandler(IBankCustomerRepository bankCustomerRepository, IMapper mapper)
        {
            _bankCustomerRepository = bankCustomerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankCustomerResponseDto>> Handle(GetAllBankCustomersQuery request, CancellationToken cancellationToken)
        {
            var spec = new BankCustomerGetAllSpec();
            var customers = await _bankCustomerRepository.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<BankCustomerResponseDto>>(customers);
        }
    }
}
