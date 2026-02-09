using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.Application.Interfaces;

namespace Account.Application.Commands
{
    public class UpdateBankAccountCommand : ICommand
    {
        public Guid Id { get; set; }
        public BankAccountStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
