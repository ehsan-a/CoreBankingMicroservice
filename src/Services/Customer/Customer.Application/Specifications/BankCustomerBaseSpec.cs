using Customer.Domain.Aggregates.BankCustomerAggregate;
using Shared.Application.Specifications;

namespace Customer.Application.Specifications
{
    public abstract class BankCustomerBaseSpec : Specification<BankCustomer>
    {
        protected BankCustomerBaseSpec() { }
    }
}
