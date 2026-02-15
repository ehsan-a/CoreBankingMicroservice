using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Replicas
{
    public interface ICustomerReplicaRepository : IRepository<CustomerReplica>
    {
        Task<CustomerReplica> AddAsync(CustomerReplica customer, CancellationToken cancellationToken = default);
        CustomerReplica Update(CustomerReplica customer);
        void Delete(CustomerReplica customer);
        Task<CustomerReplica?> GetByIdAsync(Guid id, ISpecification<CustomerReplica> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<CustomerReplica>> GetAllAsync(ISpecification<CustomerReplica> spec, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<CustomerReplica?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    }
}
