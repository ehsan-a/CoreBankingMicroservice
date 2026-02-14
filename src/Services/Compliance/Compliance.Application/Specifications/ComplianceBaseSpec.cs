using Compliance.Domain.Aggregates.BankComplinceAggregate;
using Shared.Application.Specifications;

namespace Compliance.Application.Specifications
{
    public abstract class ComplianceBaseSpec : Specification<BankCompliance>
    {
        protected ComplianceBaseSpec() { }
    }
}
