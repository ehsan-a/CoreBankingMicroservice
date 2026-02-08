using Account.Domain.Aggregates.AccountAggregate;
using Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Commands
{
    public class UpdateAccountCommand : ICommand
    {
        public Guid Id { get; set; }
        public AccountStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
