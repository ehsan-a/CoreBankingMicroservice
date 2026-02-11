using Customer.Application.DTOs;
using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetAllBankCustomersQuery : IQuery<IEnumerable<BankCustomerResponseDto>>
    {
    }
}
