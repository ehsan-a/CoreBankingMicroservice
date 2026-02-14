namespace Compliance.Application.DTOs
{
    public class ComplianceResponseDto
    {
        public CivilRegistryResponseDto? CivilRegistry { get; set; }
        public CentralBankCreditCheckResponseDto? CentralBankCreditCheck { get; set; }
        public PoliceClearanceResponseDto? PoliceClearance { get; set; }
        public RegisteredComplianceResponseDto? RegisteredCompliance { get; set; }
    }
}
