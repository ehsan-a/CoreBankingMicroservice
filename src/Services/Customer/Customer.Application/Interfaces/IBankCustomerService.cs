using Customer.Application.DTOs;
using Customer.Domain.Aggregates.BankCustomerAggregate;
using System.Security.Claims;

namespace Customer.Application.Interfaces
{
    public interface IBankCustomerService
    {
        Task<BankCustomerResponseDto> CreateAsync(CreateBankCustomerRequest request, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<IEnumerable<BankCustomerResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<BankCustomerResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateBankCustomerRequest request, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<BankCustomer?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
