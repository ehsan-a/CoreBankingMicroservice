using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Interfaces;

namespace Customer.Application.Queries
{
    public class GetBankCustomerByNationalCodeQuery : IQuery<BankCustomer>
    {
        public string NationalCode { get; set; }
    }
}
