using Account.Domain.Aggregates.BankAccountAggregate;
using Account.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Specifications;

namespace Account.Infrastructure.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AccountDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public BankAccountRepository(AccountDbContext context)
        {
            _context = context;
        }
        public async Task<BankAccount> AddAsync(BankAccount bankAccount, CancellationToken cancellationToken = default)
        {
            return (await _context.BankAccounts.AddAsync(bankAccount, cancellationToken)).Entity;
        }

        public BankAccount Update(BankAccount bankAccount)
        {
            return _context.BankAccounts.Update(bankAccount).Entity;
        }

        public void Delete(BankAccount bankAccount)
        {
            if (bankAccount is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.BankAccounts.Update(bankAccount);
            }
            else
            {
                _context.BankAccounts.Remove(bankAccount);
            }
        }
        public async Task<IEnumerable<BankAccount>> GetAllAsync(ISpecification<BankAccount> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.BankAccounts.AsQueryable();
            queryable = SpecificationEvaluator<BankAccount>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<BankAccount?> GetByIdAsync(Guid id, ISpecification<BankAccount> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.BankAccounts.AsQueryable();
            queryable = SpecificationEvaluator<BankAccount>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.BankAccounts
                       .AsNoTracking()
                       .AnyAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<BankAccount?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.BankAccounts
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
