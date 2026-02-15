using Account.Domain.Aggregates.BankAccountAggregate;
using Account.Domain.Replicas;
using Account.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Specifications;

namespace Account.Infrastructure.Repositories
{
    public class CustomerReplicaRepository : ICustomerReplicaRepository
    {
        private readonly AccountDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public CustomerReplicaRepository(AccountDbContext context)
        {
            _context = context;
        }
        public async Task<CustomerReplica> AddAsync(CustomerReplica customer, CancellationToken cancellationToken = default)
        {
            return (await _context.CustomerReplicas.AddAsync(customer, cancellationToken)).Entity;
        }

        public CustomerReplica Update(CustomerReplica customer)
        {
            return _context.CustomerReplicas.Update(customer).Entity;
        }

        public void Delete(CustomerReplica customer)
        {
            if (customer is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.CustomerReplicas.Update(customer);
            }
            else
            {
                _context.CustomerReplicas.Remove(customer);
            }
        }
        public async Task<IEnumerable<CustomerReplica>> GetAllAsync(ISpecification<CustomerReplica> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.CustomerReplicas.AsQueryable();
            queryable = SpecificationEvaluator<CustomerReplica>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<CustomerReplica?> GetByIdAsync(Guid id, ISpecification<CustomerReplica> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.CustomerReplicas.AsQueryable();
            queryable = SpecificationEvaluator<CustomerReplica>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.CustomerReplicas
                       .AsNoTracking()
                       .AnyAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<CustomerReplica?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.CustomerReplicas
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
