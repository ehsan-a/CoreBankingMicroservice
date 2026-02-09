using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Aggregates.BankAccountAggregate
{
    public enum BankAccountStatus
    {
        Active = 1,
        Blocked = 2,
        Closed = 3
    }
}
