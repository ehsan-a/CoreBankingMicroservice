using Shared.Application.Interfaces;

namespace Customer.Application.Commands
{
    public class UpdateBankCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
    }
}
