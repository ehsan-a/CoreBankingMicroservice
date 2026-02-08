using Account.Domain.Aggregates.AccountAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.DTOs
{
    public class UpdateAccountRequestDto
    {
        public Guid Id { get; set; }
        public AccountStatus Status { get; set; }
    }
}
