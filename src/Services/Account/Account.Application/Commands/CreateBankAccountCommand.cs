using Account.Application.DTOs;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Commands
{
    public class CreateBankAccountCommand : ICommand<BankAccountResponseDto>
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
    }
}
