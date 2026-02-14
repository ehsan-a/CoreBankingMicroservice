using Compliance.Application.DTOs;
using Shared.Application.Interfaces;

namespace Compliance.Application.Queries
{
    public class GetComplianceByNationalCodeQuery : IQuery<ComplianceResponseDto>
    {
        public string NationalCode { get; set; }
    }
}
