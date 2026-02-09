using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.EventBus.Abstractions
{
    public interface IEventBusBuilder
    {
        public IServiceCollection Services { get; }
    }
}
