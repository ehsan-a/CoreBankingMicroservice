using Audit.Domain.Aggregates.AuditLogAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task LogAsync(AuditLog auditLog, CancellationToken cancellationToken = default);
    }
}
