using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.Application.DTOs
{
    public class CreateComplianceRequest
    {
        public string NationalCode { get; set; }
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
    }
}
