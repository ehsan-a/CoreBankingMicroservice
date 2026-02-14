
using Compliance.Application.DTOs;

namespace Compliance.Application.Interfaces
{
    public interface ICentralBankCreditCheckService
    {
        Task<CentralBankCreditCheckResponseDto?> GetResultInfoAsync(string nationalCode);
    }
}
