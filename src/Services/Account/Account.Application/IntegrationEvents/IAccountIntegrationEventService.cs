using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.IntegrationEvents
{
    public interface IAccountIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
