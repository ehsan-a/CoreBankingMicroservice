using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Commands
{
    public class DeleteAccountCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
