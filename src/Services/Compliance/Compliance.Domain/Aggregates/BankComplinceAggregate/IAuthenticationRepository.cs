using Shared.Domain.Interfaces;

namespace Compliance.Domain.Aggregates.BankComplinceAggregate
{
    public interface IComplianceRepository : IRepository<BankCompliance>
    {
        Task<BankCompliance?> GetByNationalCodeAsync(string nationalCode, ISpecification<BankCompliance> spec, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<BankCompliance> AddAsync(BankCompliance bankCompliance, CancellationToken cancellationToken = default);

    }
}