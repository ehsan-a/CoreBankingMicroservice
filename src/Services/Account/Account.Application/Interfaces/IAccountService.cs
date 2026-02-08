using Account.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Account.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponseDto> CreateAsync(CreateAccountRequestDto createAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<IEnumerable<AccountResponseDto>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken);
        Task<AccountResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateAccountRequestDto updateAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
