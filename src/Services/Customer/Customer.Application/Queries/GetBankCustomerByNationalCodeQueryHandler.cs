using Customer.Application.Specifications;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetBankCustomerByNationalCodeQueryHandler : IQueryHandler<GetBankCustomerByNationalCodeQuery, BankCustomer>
    {
        private readonly IBankCustomerRepository _bankCustomerRepository;

        public GetBankCustomerByNationalCodeQueryHandler(IBankCustomerRepository bankCustomerRepository)
        {
            _bankCustomerRepository = bankCustomerRepository;
        }

        public async Task<BankCustomer?> Handle(GetBankCustomerByNationalCodeQuery request, CancellationToken cancellationToken)
        {
            var spec = new BankCustomerGetAllSpec();

            return await _bankCustomerRepository.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
        }
    }
}
