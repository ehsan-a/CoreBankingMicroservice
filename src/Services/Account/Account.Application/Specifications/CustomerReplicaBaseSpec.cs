using Account.Domain.Aggregates.BankAccountAggregate;
using Account.Domain.Replicas;
using Shared.Application.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.Specifications
{
    public class CustomerReplicaBaseSpec : Specification<CustomerReplica>
    {
        protected CustomerReplicaBaseSpec()
        {

        }
    }
}
