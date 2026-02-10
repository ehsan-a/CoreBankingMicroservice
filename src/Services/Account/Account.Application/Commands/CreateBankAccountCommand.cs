using Account.Application.DTOs;
using Shared.Application.Interfaces;

namespace Account.Application.Commands
{
    public class CreateBankAccountCommand : ICommand<bool>
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
    }
}
