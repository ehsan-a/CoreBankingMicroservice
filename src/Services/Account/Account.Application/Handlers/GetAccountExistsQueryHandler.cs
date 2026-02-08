
using Account.Application.Queries;
using Account.Domain.Aggregates.AccountAggregate;
using Shared.Application.Interfaces;

namespace Account.Application.Handlers
{
    public class GetAccountExistsQueryHandler : IQueryHandler<GetAccountExistsQuery, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountExistsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(GetAccountExistsQuery request, CancellationToken cancellationToken)
        {
            return await _accountRepository.ExistsByIdAsync(request.Id, cancellationToken);
        }
    }
}
