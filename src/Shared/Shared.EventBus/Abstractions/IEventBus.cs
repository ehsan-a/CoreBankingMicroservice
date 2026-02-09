using Shared.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}
