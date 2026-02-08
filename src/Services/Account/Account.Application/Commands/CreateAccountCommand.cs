using Account.Application.DTOs;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Commands
{
    public class CreateAccountCommand : ICommand<AccountResponseDto>
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
    }
}
