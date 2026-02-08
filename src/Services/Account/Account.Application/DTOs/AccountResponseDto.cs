using Account.Domain.Aggregates.AccountAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.DTOs
{
    public class AccountResponseDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = default!;

        public decimal Balance { get; set; }

        public AccountStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CustomerId { get; set; }
    }
}
