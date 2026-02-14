
using Compliance.Application.DTOs;

namespace Compliance.Application.Interfaces
{
    public interface IPoliceClearanceService
    {
        Task<PoliceClearanceResponseDto?> GetResultInfoAsync(string nationalCode);
    }
}
