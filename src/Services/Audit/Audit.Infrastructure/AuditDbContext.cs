using Audit.Domain.Aggregates.AuditLogAggregate;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure
{
    public class AuditDbContext : DbContext
    {
        public AuditDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AuditDbContext()
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; } = default!;
    }
}
