using Compliance.Application.DTOs;
using Shared.Application.Interfaces;

namespace Compliance.Application.Commands
{
    public class CreateComplianceCommand : ICommand<RegisteredComplianceResponseDto>
    {
        public string NationalCode { get; set; }
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
        public Guid UserId { get; set; }
    }
}
