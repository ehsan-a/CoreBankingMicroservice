using Account.Domain.Aggregates.BankAccountAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.DTOs
{
    public class UpdateBankAccountRequestDto
    {
        public Guid Id { get; set; }
        public BankAccountStatus Status { get; set; }
    }
}
