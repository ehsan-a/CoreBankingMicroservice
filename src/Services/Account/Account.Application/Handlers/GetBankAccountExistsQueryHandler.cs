
using Account.Application.Queries;
using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.Application.Interfaces;

namespace Account.Application.Handlers
{
    public class GetBankAccountExistsQueryHandler : IQueryHandler<GetBankAccountExistsQuery, bool>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public GetBankAccountExistsQueryHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<bool> Handle(GetBankAccountExistsQuery request, CancellationToken cancellationToken)
        {
            return await _bankAccountRepository.ExistsByIdAsync(request.Id, cancellationToken);
        }
    }
}
