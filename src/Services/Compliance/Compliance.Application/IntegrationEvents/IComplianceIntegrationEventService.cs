using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compliance.Application.IntegrationEvents
{
    public interface IComplianceIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
