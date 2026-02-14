using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Compliance.Infrastructure.Idempotency
{
    public class ClientRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
