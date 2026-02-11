using Audit.Application.Interfaces;
using Audit.Domain.Aggregates.AuditLogAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Infrastructure
{
    public class AuditLogService : IAuditLogService
    {
        private readonly AuditDbContext _context;

        public AuditLogService(AuditDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(AuditLog auditLog, CancellationToken cancellationToken = default)
        {
            await _context.AuditLogs.AddAsync(auditLog, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
