using Shared.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Replicas
{
    public class CustomerReplica : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; }
        public CustomerStatus Status { get; set; }
    }

    public enum CustomerStatus
    {
        Active,
        DeActive
    }
}
