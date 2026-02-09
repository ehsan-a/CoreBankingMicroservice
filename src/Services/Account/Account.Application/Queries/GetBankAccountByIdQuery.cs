using Account.Application.DTOs;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Queries
{
    public class GetBankAccountByIdQuery : IQuery<BankAccountResponseDto>
    {
        public Guid Id { get; set; }
    }
}
