using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Domain.Aggregates.AuditLogAggregate
{
    public enum AuditActionType
    {
        Create,
        Update,
        Delete,
        Login,
        Logout
    }
}
