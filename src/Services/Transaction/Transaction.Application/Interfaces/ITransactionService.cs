using System.Security.Claims;
using Transaction.Application.DTOs;

namespace Transaction.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponseDto> CreateAsync(CreateTransactionRequestDto createTransactionRequestDto, ClaimsPrincipal principal, string idempotencyKey, CancellationToken cancellationToken);
        Task<IEnumerable<TransactionResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<TransactionResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
