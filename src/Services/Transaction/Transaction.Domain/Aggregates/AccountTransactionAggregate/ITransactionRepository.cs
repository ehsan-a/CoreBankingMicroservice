using Shared.Domain.Interfaces;

namespace Transaction.Domain.Aggregates.AccountTransactionAggregate
{
    public interface ITransactionRepository : IRepository<AccountTransaction>
    {
        Task<AccountTransaction?> GetByIdAsync(Guid id, ISpecification<AccountTransaction> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<AccountTransaction>> GetAllAsync(ISpecification<AccountTransaction> spec, CancellationToken cancellationToken = default);
        Task<AccountTransaction> AddAsync(AccountTransaction transaction, CancellationToken cancellationToken = default);
    }
}