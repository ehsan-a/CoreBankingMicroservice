using Account.Domain.Aggregates.BankAccountAggregate;
using Shared.Application.Specifications;

namespace Account.Application.Specifications
{
    public abstract class BankAccountBaseSpec : Specification<BankAccount>
    {
        protected BankAccountBaseSpec()
        {

        }
    }
}
