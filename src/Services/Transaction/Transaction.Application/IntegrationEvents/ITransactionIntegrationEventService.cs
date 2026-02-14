using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Application.IntegrationEvents
{
    public interface ITransactionIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
