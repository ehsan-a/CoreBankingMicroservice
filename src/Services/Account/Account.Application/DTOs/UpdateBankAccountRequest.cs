using Account.Domain.Aggregates.BankAccountAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.DTOs
{
    public record UpdateBankAccountRequest(

     Guid Id,
     BankAccountStatus Status);
}
