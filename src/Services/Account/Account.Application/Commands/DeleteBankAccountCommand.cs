using Shared.Application.Interfaces;

namespace Account.Application.Commands
{
    public class DeleteBankAccountCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
