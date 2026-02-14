using Shared.Application.Specifications;
using Transaction.Domain.Aggregates.AccountTransactionAggregate;

namespace Transaction.Application.Specifications
{
    public abstract class TransactionBaneSpec : Specification<AccountTransaction>
    {
        protected TransactionBaneSpec() { }
    }
}
