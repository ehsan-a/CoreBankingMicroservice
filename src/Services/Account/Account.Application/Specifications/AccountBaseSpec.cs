using Shared.Application.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Specifications
{
    public abstract class AccountBaseSpec : Specification<Domain.Aggregates.AccountAggregate.Account>
    {
        protected AccountBaseSpec()
        {

        }
    }
}
