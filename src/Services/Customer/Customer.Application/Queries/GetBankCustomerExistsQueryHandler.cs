using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetBankCustomerExistsQueryHandler : IQueryHandler<GetBankCustomerExistsQuery, bool>
    {
        private readonly IBankCustomerRepository _bankCustomerRepository;

        public GetBankCustomerExistsQueryHandler(IBankCustomerRepository bankCustomerRepository)
        {
            _bankCustomerRepository = bankCustomerRepository;
        }

        public async Task<bool> Handle(GetBankCustomerExistsQuery request, CancellationToken cancellationToken)
        {
            return await _bankCustomerRepository.ExistsByIdAsync(request.Id, cancellationToken);
        }
    }
}