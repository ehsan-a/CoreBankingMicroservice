using Compliance.Application.DTOs;
using Shared.Application.Interfaces;

namespace Compliance.Application.Queries
{
    public class GetInquiryComplianceQuery : IQuery<ComplianceResponseDto>
    {
        public string NationalCode { get; set; }
    }
}
