using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Compliance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Specifications;

namespace Compliance.Infrastructure.Repositories
{
    public class ComplianceRepository : IComplianceRepository
    {
        private readonly ComplianceDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public ComplianceRepository(ComplianceDbContext context)
        {
            _context = context;
        }

        public async Task<BankCompliance> AddAsync(BankCompliance bankCompliance, CancellationToken cancellationToken = default)
        {
            return (await _context.BankCompliances.AddAsync(bankCompliance, cancellationToken)).Entity;
        }

        public async Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _context.BankCompliances
                        .AsNoTracking()
                        .AnyAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }

        public async Task<BankCompliance?> GetByNationalCodeAsync(string nationalCode, ISpecification<BankCompliance> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.BankCompliances.AsQueryable();
            queryable = SpecificationEvaluator<BankCompliance>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }
    }
}
