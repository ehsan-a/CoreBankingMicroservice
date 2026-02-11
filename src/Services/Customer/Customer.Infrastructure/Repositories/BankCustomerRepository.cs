using Customer.Domain.Aggregates.BankCustomerAggregate;
using Customer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Specifications;

namespace CoreBanking.Infrastructure.Repositories
{
    public class BankCustomerRepository : IBankCustomerRepository
    {
        private readonly CustomerDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public BankCustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<BankCustomer> AddAsync(BankCustomer customer, CancellationToken cancellationToken = default)
        {
            return (await _context.BankCustomers.AddAsync(customer, cancellationToken)).Entity;
        }

        public void Delete(BankCustomer customer)
        {
            if (customer is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.BankCustomers.Update(customer);
            }
            else
            {
                _context.BankCustomers.Remove(customer);
            }
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.BankCustomers
                       .AsNoTracking()
                       .AnyAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _context.BankCustomers
                     .AsNoTracking()
                     .AnyAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }

        public async Task<IEnumerable<BankCustomer>> GetAllAsync(ISpecification<BankCustomer> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.BankCustomers.AsQueryable();
            queryable = SpecificationEvaluator<BankCustomer>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<BankCustomer?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.BankCustomers
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<BankCustomer?> GetByIdAsync(Guid id, ISpecification<BankCustomer> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.BankCustomers.AsQueryable();
            queryable = SpecificationEvaluator<BankCustomer>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<BankCustomer?> GetByNationalCodeAsync(string nationalCode, ISpecification<BankCustomer> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.BankCustomers.AsQueryable();
            queryable = SpecificationEvaluator<BankCustomer>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }

        public BankCustomer Update(BankCustomer customer)
        {
            return _context.BankCustomers.Update(customer).Entity;
        }
    }
}
