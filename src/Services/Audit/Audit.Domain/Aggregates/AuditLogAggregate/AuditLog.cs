using Shared.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Domain.Aggregates.AuditLogAggregate
{
    public class AuditLog : IAggregateRoot
    {
        public AuditLog(string entityName, Guid? entityId, AuditActionType actionType, string performedBy, string? oldValue, string? newValue)
        {
            EntityName = entityName;
            EntityId = entityId;
            ActionType = actionType;
            PerformedBy = performedBy;
            OldValue = oldValue;
            NewValue = newValue;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private AuditLog() { }

        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public Guid? EntityId { get; set; }
        public AuditActionType ActionType { get; set; }
        public string PerformedBy { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
