using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.Application.DTOs
{
    public class CentralBankCreditCheckResponseDto
    {
        public bool IsValid { get; set; }

        public string? Reason { get; set; }

        public DateTime CheckedAt { get; set; }
    }
}
