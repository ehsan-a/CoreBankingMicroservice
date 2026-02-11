using Shared.Application.Interfaces;

namespace Customer.Application.Commands
{
    public class DeleteBankCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
