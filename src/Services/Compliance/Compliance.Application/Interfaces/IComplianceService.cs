using Compliance.Application.DTOs;
using System.Security.Claims;

namespace Compliance.Application.Interfaces
{
    public interface IComplianceService
    {
        Task<RegisteredComplianceResponseDto> CreateAsync(CreateComplianceRequest request, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<ComplianceResponseDto?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(string nationalCode, CancellationToken cancellationToken);
        Task<ComplianceResponseDto> GetInquiryAsync(string nationalCode, CancellationToken cancellationToken);
    }
}
