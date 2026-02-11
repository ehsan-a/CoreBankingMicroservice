using Customer.Application.DTOs;
using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetBankCustomerByIdQuery : IQuery<BankCustomerResponseDto>
    {
        public Guid Id { get; set; }
    }
}
