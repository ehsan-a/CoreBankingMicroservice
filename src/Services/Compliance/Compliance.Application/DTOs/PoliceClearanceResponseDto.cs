using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.Application.DTOs
{
    public class PoliceClearanceResponseDto
    {
        public bool HasCriminalRecord { get; set; }

        public string? Description { get; set; }

        public DateTime CheckedAt { get; set; }
    }
}
