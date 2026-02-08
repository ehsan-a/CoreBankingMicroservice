using Account.Domain.Aggregates.AccountAggregate;
using Account.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public AccountRepository(AccountDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Aggregates.AccountAggregate.Account> AddAsync(Domain.Aggregates.AccountAggregate.Account account, CancellationToken cancellationToken = default)
        {
            return (await _context.Accounts.AddAsync(account, cancellationToken)).Entity;
        }

        public Domain.Aggregates.AccountAggregate.Account Update(Domain.Aggregates.AccountAggregate.Account account)
        {
            return _context.Accounts.Update(account).Entity;
        }

        public void Delete(Domain.Aggregates.AccountAggregate.Account account)
        {
            if (account is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.Accounts.Update(account);
            }
            else
            {
                _context.Accounts.Remove(account);
            }
        }
        public async Task<IEnumerable<Domain.Aggregates.AccountAggregate.Account>> GetAllAsync(ISpecification<Domain.Aggregates.AccountAggregate.Account> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Accounts.AsQueryable();
            queryable = SpecificationEvaluator<Domain.Aggregates.AccountAggregate.Account>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<Domain.Aggregates.AccountAggregate.Account?> GetByIdAsync(Guid id, ISpecification<Domain.Aggregates.AccountAggregate.Account> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Accounts.AsQueryable();
            queryable = SpecificationEvaluator<Domain.Aggregates.AccountAggregate.Account>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                       .AsNoTracking()
                       .AnyAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<Domain.Aggregates.AccountAggregate.Account?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
