using Customer.Application.DTOs;
using Shared.Application.Interfaces;

namespace Customer.Application.Commands
{
    public class CreateBankCustomerCommand : ICommand<BankCustomerResponseDto>
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
    }
}
