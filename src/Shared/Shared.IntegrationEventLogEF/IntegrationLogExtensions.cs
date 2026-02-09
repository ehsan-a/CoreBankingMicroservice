using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.IntegrationEventLogEF
{
    public static class IntegrationLogExtensions
    {
        public static void UseIntegrationEventLogs(this ModelBuilder builder)
        {
            builder.Entity<IntegrationEventLogEntry>(builder =>
            {
                builder.ToTable("IntegrationEventLog");

                builder.HasKey(e => e.EventId);
            });
        }
    }
}
