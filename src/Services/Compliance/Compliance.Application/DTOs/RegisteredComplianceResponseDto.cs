using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.Application.DTOs
{
    public class RegisteredComplianceResponseDto
    {
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
