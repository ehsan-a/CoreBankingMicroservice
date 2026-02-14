using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Specifications;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;
using Transaction.Infrastructure.Persistence;

namespace Transaction.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task<AccountTransaction> AddAsync(AccountTransaction transaction, CancellationToken cancellationToken = default)
        {
            return (await _context.AccountTransactions.AddAsync(transaction, cancellationToken)).Entity;
        }

        public async Task<IEnumerable<AccountTransaction>> GetAllAsync(ISpecification<AccountTransaction> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.AccountTransactions.AsQueryable();
            queryable = SpecificationEvaluator<AccountTransaction>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<AccountTransaction?> GetByIdAsync(Guid id, ISpecification<AccountTransaction> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.AccountTransactions.AsQueryable();
            queryable = SpecificationEvaluator<AccountTransaction>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
