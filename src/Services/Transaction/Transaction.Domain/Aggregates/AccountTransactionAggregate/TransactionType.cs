using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Domain.Aggregates.AccountTransactionAggregate
{
    public enum TransactionType
    {
        Transfer, Deposit, Withdrawal
    }

}
