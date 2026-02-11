using Shared.Domain.Interfaces;

namespace Customer.Domain.Aggregates.BankCustomerAggregate
{
    public interface IBankCustomerRepository : IRepository<BankCustomer>
    {
        Task<BankCustomer?> GetByIdAsync(Guid id, ISpecification<BankCustomer> spec, CancellationToken cancellationToken = default);
        Task<BankCustomer?> GetByNationalCodeAsync(string nationalCode, ISpecification<BankCustomer> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<BankCustomer>> GetAllAsync(ISpecification<BankCustomer> spec, CancellationToken cancellationToken = default);
        Task<BankCustomer> AddAsync(BankCustomer customer, CancellationToken cancellationToken = default);
        BankCustomer Update(BankCustomer customer);
        void Delete(BankCustomer customer);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<BankCustomer?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    }
}