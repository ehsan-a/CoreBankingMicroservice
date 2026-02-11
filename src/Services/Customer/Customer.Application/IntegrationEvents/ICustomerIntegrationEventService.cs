using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.IntegrationEvents
{
    public interface ICustomerIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
