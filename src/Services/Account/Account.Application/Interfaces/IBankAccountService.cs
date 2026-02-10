using Account.Application.DTOs;
using System.Security.Claims;

namespace Account.Application.Interfaces
{
    public interface IBankAccountService
    {
        Task<bool> CreateAsync(CreateBankAccountRequestDto dto, Guid requestId, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<IEnumerable<BankAccountResponseDto>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken);
        Task<BankAccountResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateBankAccountRequestDto dto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
