using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
     where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);

        Task IIntegrationEventHandler.Handle(IntegrationEvent @event) => Handle((TIntegrationEvent)@event);
    }

    public interface IIntegrationEventHandler
    {
        Task Handle(IntegrationEvent @event);
    }
}
