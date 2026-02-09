using Shared.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Aggregates.BankAccountAggregate
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        Task<BankAccount> AddAsync(BankAccount account, CancellationToken cancellationToken = default);
        BankAccount Update(BankAccount account);
        void Delete(BankAccount account);
        Task<BankAccount?> GetByIdAsync(Guid id, ISpecification<BankAccount> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<BankAccount>> GetAllAsync(ISpecification<BankAccount> spec, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BankAccount?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    }
}
